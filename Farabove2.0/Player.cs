using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -800.0f;
	private AudioStreamPlayer2D shoot;
	private AnimatedSprite2D animation;

	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		shoot = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (animation != null)
		{
			animation.Play();
		}

		// Reset X and Y velocity
		velocity = Vector2.Zero;

		// Handle horizontal movement
		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += Speed;
		}
		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= Speed;
		}

		// Handle vertical movement
		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= Speed;
		}
		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += Speed;
		}

		// Handle attack action
		if (Input.IsActionPressed("Attack1"))
		{
			if (!shoot.Playing)
			{
				shoot.Play();
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
