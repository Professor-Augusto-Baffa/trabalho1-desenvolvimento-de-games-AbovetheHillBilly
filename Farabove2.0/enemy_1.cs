using Godot;
using System;

public partial class enemy_1 : CharacterBody2D
{
	Player Player;
	[Export] float Speed = 250f;
	[Export] float damage = 10f;
	[Export] float aps = 2f;

	float attack_speed;
	float time_until_attack;
	bool within_attack_range = false;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Player = (Player)GetTree().Root.GetNode("Main").GetNode("Player");
		attack_speed = 1 / aps;
		time_until_attack = attack_speed;
	}

	public override void _Process(double delta)
	{
		if (within_attack_range && time_until_attack <= 0)
		{
			Attack();
			time_until_attack = attack_speed;
		}
		else
		{
			time_until_attack -= (float)delta;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
			LookAt(Player.GlobalPosition);
			Vector2 direction = (Player.GlobalPosition - GlobalPosition).Normalized();
			Velocity = direction * Speed;
		MoveAndSlide();
	}

	public void Attack()
	{
		Player.GetNode<Health>("Health").Damage(damage);
	}

	public void OnAttackRanngeBodyEnter(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			within_attack_range = true;
		}
	}

	public void OnAttackRanngeBodyExit(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			within_attack_range = false;
			time_until_attack = attack_speed;
		}
	}
}
