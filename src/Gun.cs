using Godot;
using System;

public class Gun : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Sprite sprite;
	RayCast raycast;
	AnimationPlayer animPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<CanvasLayer>("CanvasLayer").GetNode<Control>("Control").GetNode<Sprite>("Sprite");
		raycast = GetNode<RayCast>("RayCast");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public Vector3 Shoot()
	{
		animPlayer.Play("shoot");
		if(raycast.IsColliding() &&  ((Node)raycast.GetCollider()).IsInGroup("enemies"))
		{
			GD.Print("Enemy");
			Enemy body = (Enemy)raycast.GetCollider();
			body.damage(10.0f);
		}
		return raycast.GetCollisionPoint();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(raycast.IsColliding())
			GD.Print(raycast.GetCollider());
	}
}
