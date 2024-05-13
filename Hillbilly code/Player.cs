using Godot;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400; // How fast the player will move (pixels/sec).
	
	[Export]
	public PackedScene ProjectileScene{get; private set;}
	
	[Export]
	public  PackedScene Particles{get; private set;}
	
	[Signal]
	public delegate void HitEventHandler();
	
	public Vector2 ScreenSize; // Size of the game window.
	
	PackedScene bulletScene;
	
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		bulletScene = GD.Load<PackedScene>("res://Bullet.tscn");
	}
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("move_up")|| Input.IsActionJustPressed("move_right")||Input.IsActionJustPressed("move_left")||Input.IsActionJustPressed("move_down")){
			Steam jet = Particles.Instantiate<Steam>();

			jet.Rotation = GlobalRotation;
			jet.GlobalPosition = GlobalPosition;
			
			GetTree().Root.AddChild(jet);
			}
		var velocity = Vector2.Zero; // The player's movement vector.
		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		if (velocity.X < 0)
		{
			animatedSprite2D.Animation = "Left";
			animatedSprite2D.Rotation = 0;
		}
		if (velocity.X > 0)
		{
			animatedSprite2D.Animation = "Right";
			animatedSprite2D.Rotation = 0;
		}
		
		else if (velocity.Y > 0)
		{
			animatedSprite2D.Animation = "Up";
			animatedSprite2D.Rotation = 0;
		}
		
		 else if (velocity.Y < 0)
		{
			animatedSprite2D.Animation = "Down";
			animatedSprite2D.Rotation = 0;
		}
		else if (Input.IsActionPressed("Attack"))
		{
			animatedSprite2D.Animation = "Attack";
			LookAt(GetGlobalMousePosition());
		}
	}
	private void OnBodyEntered(Node2D body)
	{
	Hide(); // Player disappears after being hit.
	EmitSignal(SignalName.Hit);
	// Must be deferred as we can't change physics properties on a physics callback.
	GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
}
