using Godot;
using System;

public partial class main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void NewGame(){
		var player = GetNode<mario>("mario");
		var StartPosition = GetNode<Marker2D>("StartPosition");
		player.Start(StartPosition.Position);
	}
}
