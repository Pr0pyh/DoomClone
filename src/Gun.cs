using Godot;
using System;

public class Gun : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public bool canShoot;
	public float gunFireRate;
	public float gunDamage;
	Sprite sprite;
	RayCast raycast;
	AnimationPlayer animPlayer;
	Timer timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<CanvasLayer>("CanvasLayer").GetNode<Control>("Control").GetNode<Sprite>("Sprite");
		raycast = GetNode<RayCast>("RayCast");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		timer = GetNode<Timer>("Timer");
		canShoot = true;
		gunFireRate = 1.0f;
		gunDamage = 50.0f;
	}

	public Vector3 Shoot()
	{
		animPlayer.Play("shoot");
		canShoot = false;
		timer.WaitTime = gunFireRate;
		timer.Start();
		if(raycast.IsColliding() &&  ((Node)raycast.GetCollider()).IsInGroup("enemies"))
		{
			GD.Print("Enemy");
			Enemy body = (Enemy)raycast.GetCollider();
			float result = body.damage(gunDamage);
			if(body.health < 0 && result != 0)
			{
				if(body.fireRate)
				{
					gunDamage *= 1.0f+result;
					sprite.Modulate -= new Color(0.0f, 0.1f, 0.1f, 0.0f);
				}
				else
				{
					gunFireRate *= result;
					sprite.Modulate -= new Color(0.1f, 0.1f, 0.0f, 0.0f);
				}
			}
			body.hit = true;
		}
		return raycast.GetCollisionPoint();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(float delta)
	// {
	// 	if(raycast.IsColliding())
	// 		GD.Print(raycast.GetCollider());
	// }

	public void _on_Timer_timeout()
	{
		canShoot = true;
	}
}
