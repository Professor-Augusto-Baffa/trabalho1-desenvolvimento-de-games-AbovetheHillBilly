using Godot;

public partial class world : Node
{
	// Don't forget to rebuild the project so the editor knows about the new export variable.

	[Export]
	public PackedScene MeteorScn { get; set; }
	
	
	public void NewGame()
	{
		var player = GetNode<Player>("Player");
		GetNode<Timer>("MeteorTimer").Start();
	}
	
	private void OnMeteorTimerTimeout()
	{
	Meteor meteor = MeteorScn.Instantiate<Meteor>();

			// Choose a random location on Path2D.
			var SpawnLocation = GetNode<PathFollow2D>("Path2D/MeteorPath");
			SpawnLocation.ProgressRatio = GD.RandRange(0, 400);

			// Set the mob's direction perpendicular to the path direction.
			float direction = SpawnLocation.Rotation + Mathf.Pi / 2;

			// Set the mob's position to a random location.
			meteor.Position = SpawnLocation.Position;

			// Add some randomness to the direction.
			direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
			meteor.Rotation = direction;

			// Choose the velocity.
			var velocity = new Vector2((float)GD.RandRange(150.0, 750.0), 0);
			meteor.LinearVelocity = velocity.Rotated(direction);

			// Spawn the mob by adding it to the Main scene.
			AddChild(meteor);		
	}
	
		
	public override void _Ready()
	{
		NewGame();
	}
}
