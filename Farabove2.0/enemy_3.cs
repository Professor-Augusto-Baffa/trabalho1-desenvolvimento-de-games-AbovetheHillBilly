using Godot;
using System;

public partial class enemy_3 : CharacterBody2D
{
	Player Player;
	[Export] float Speed = 250f;
	[Export] float damage = 10f;
	[Export] float aps = 2f;
	[Export] float randomnessFactor = 0.1f; // Adjust this value to control the amount of randomness

	float attack_speed;
	float time_until_attack;
	bool within_attack_range = false;
	bool within_enemy = false;

	RandomNumberGenerator rng = new RandomNumberGenerator();

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Player = (Player)GetTree().Root.GetNode("Main").GetNode("Player");
		attack_speed = 1 / aps;
		time_until_attack = attack_speed;

		// Seed the random number generator
		rng.Randomize();
	}

	public override void _Process(double delta)
	{
		if (within_attack_range && time_until_attack <= 0)
		{
			Attack(Player);
			time_until_attack = attack_speed;
		}
		else
		{
			time_until_attack -= (float)delta;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 baseDirection = Vector2.Down;
		Vector2 randomOffset = new Vector2(
			rng.RandfRange(-randomnessFactor, randomnessFactor),
			rng.RandfRange(-randomnessFactor, randomnessFactor)
		);
		Vector2 direction = (baseDirection + randomOffset).Normalized();

		Velocity = direction * Speed;
		MoveAndSlide();
	}

	public void Attack(Player target)
	{
		target.GetNode<Health>("Health").Damage(damage);
	}
	
	public void Attack(Node2D target)
	{
		target.GetNode<Health>("Health").Damage(damage);
	}
	
	public void OnAttackRangeBodyEnter(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			within_attack_range = true;
		}
		if (body.IsInGroup("enemy") && body != this)
		{
			within_enemy = true;
			Attack(body);  // Immediately attack any enemy that enters the range
		}
	}

	public void OnAttackRangeBodyExit(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			within_attack_range = false;
			time_until_attack = attack_speed;
		}
	}
}
