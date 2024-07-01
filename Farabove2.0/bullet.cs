using Godot;
using System;

public partial class bullet : RigidBody2D
{
	[Export] public float damage = 10;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
	}
	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("enemy")) {
			body.GetNode<Health>("Health").Damage(damage);
		}
		QueueFree();
	}
}
