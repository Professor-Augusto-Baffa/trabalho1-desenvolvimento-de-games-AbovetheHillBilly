using Godot;
using System;

public partial class particles : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("Explosion");
		string[] Meteortypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(Meteortypes[GD.Randi() % Meteortypes.Length]);
	}
	
	
	private void _on_explosion_animation_finished()
	{
		QueueFree();
	}

	private void _on_explosion_animation_looped()
	{
		QueueFree();
	}
}
