using Godot;
using System;

public class Gun : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Sprite sprite;
	RayCast raycast;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<CanvasLayer>("CanvasLayer").GetNode<Control>("Control").GetNode<Sprite>("Sprite");
		raycast = GetNode<RayCast>("RayCast");
	}

	public Vector3 Shoot()
	{
		sprite.Modulate = new Color(0.0f, 1.0f, 1.0f, 1.0f);
		GD.Print("Shot");
		GD.Print(raycast.GetCollider());
		return raycast.GetCollisionPoint();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(raycast.IsColliding())
			GD.Print(raycast.GetCollider());
	}
}
