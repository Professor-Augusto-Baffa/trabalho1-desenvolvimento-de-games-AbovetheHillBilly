using Godot;
using System;

public partial class Meteor : RigidBody2D
{
	[Export]
	public PackedScene ParticlesScn  {get; set;}
	// Called when the node enters the scene tree for the first time.
	[Export]
	public PackedScene Bullets  {get; set;}
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("Meteors");
		RigidBody2D bullet = Bullets.Instantiate<RigidBody2D>();
		animatedSprite2D.Play();
	}
	
	private void _on_visible_on_screen_enabler_2d_screen_exited()
	{
		QueueFree();
	}
}
