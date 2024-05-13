using Godot;
using System;

public partial class Jet : Node2D
{
	[Export]
	PackedScene Jet_scn;
	
	public override void _Ready() {
	}
	public override void _Process(double delta)
	{
	if (Input.IsActionJustPressed("move_up")|| Input.IsActionJustPressed("move_right")||Input.IsActionJustPressed("move_left")||Input.IsActionJustPressed("move_down")){
		RigidBody2D jet = Jet_scn.Instantiate<RigidBody2D>();

		jet.Rotation = GlobalRotation;
		jet.GlobalPosition = GlobalPosition;
		
		GetTree().Root.AddChild(jet);
		
		}
	}
}
