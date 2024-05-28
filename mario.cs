using Godot;
using System;
using System.Security;

public partial class mario : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public Vector2 ScreenSize;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// set player velocity and bind to keys. Increase velocity on input.
		var velocity = Vector2.Zero;

		if(Input.IsActionPressed("move_right")){
			velocity.X += 1;
		}

		if(Input.IsActionPressed("move_left")){
			velocity.X -= 1;
		}

		if(Input.IsActionPressed("move_up")){
			velocity.Y += 1;
		}

		if(Input.IsActionPressed("move_down")){
			velocity.Y -= 1;
		}

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if(velocity.Length() >0){
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		} else{
			animatedSprite2D.Stop();
		}

		if(velocity.X != 0){
			animatedSprite2D.Animation = "Walk";
			animatedSprite2D.FlipV = false;
			animatedSprite2D.FlipH = velocity.X < 0;
		} else if (velocity.Y != 0){
			animatedSprite2D.Animation = "jump";
			animatedSprite2D.FlipV = false;
			animatedSprite2D.FlipH = false;
		}

		Position += velocity*(float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Math.Clamp(Position.Y, 0, ScreenSize.Y)
		);

	}

	public void Start(Vector2 position){
		Position = position;
		Show();
	}
}
