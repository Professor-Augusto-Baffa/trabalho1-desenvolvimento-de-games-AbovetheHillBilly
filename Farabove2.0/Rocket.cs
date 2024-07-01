using Godot;
using System;

public partial class Rocket : Node2D
{
	[Export]
	PackedScene bullet_scn;

	[Export]
	float bullet_speed = 600f;
	[Export]
	float bps = 5f;
	[Export]
	float bullet_damage = 30f;

	float fire_rate;
	private AudioStreamPlayer2D shoot;
	double time_until_fire = 0f;

	public override void _Ready()
	{
		fire_rate = 1 / bps;
		shoot = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Attack1") && time_until_fire > fire_rate)
		{
			RigidBody2D bullet = bullet_scn.Instantiate<RigidBody2D>();

			if (bullet != null)
			{
				bullet.Rotation = -Mathf.Pi / 2; // Set rotation to 90 degrees upwards
				bullet.GlobalPosition = GlobalPosition;
				bullet.LinearVelocity = new Vector2(0, -bullet_speed); // Move upwards

				GetTree().Root.AddChild(bullet);
				shoot.Play(); // Play the shooting sound

				time_until_fire = 0f;
			}
		}
		else
		{
			time_until_fire += delta;
		}
	}
}
