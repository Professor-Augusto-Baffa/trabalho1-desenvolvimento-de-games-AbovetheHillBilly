using Godot;
using System;

public partial class Steam : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play();
	}
	public void _on_animated_sprite_2d_animation_finished()
	{
		QueueFree();
	}
	private void _on_animated_sprite_2d_animation_looped()
	{
		QueueFree();
	}
}
