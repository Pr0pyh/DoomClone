using Godot;
using System;

public class Gun : Spatial
{
	[Export]
	public Resource gunStats;
	
	public bool canShoot;
	public float gunFireRate;
	public float gunDamage;
	Sprite sprite;
	RayCast raycast;
	public AnimationPlayer animPlayer;
	Timer timer;
	Player player;
	AudioStreamPlayer audioPlayer;
	AudioStream gunCock;
	AudioStream gunShot;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<CanvasLayer>("CanvasLayer").GetNode<Control>("Control").GetNode<Sprite>("Sprite");
		raycast = GetNode<RayCast>("RayCast");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		timer = GetNode<Timer>("Timer");
		player = GetParent().GetParent<Player>();
		audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		gunCock = (AudioStream)ResourceLoader.Load("res://assets/music/GunCockSingle PE1096303.mp3");
		gunShot = (AudioStream)ResourceLoader.Load("res://assets/music/GunShotSnglFireIn PE1097304.mp3");
		canShoot = true;
		audioPlayer.Stream = gunCock;
		audioPlayer.Play();
		//gunFireRate = 1.0f;
		//gunDamage = 50.0f;
		if (gunStats is GunStats stats)
		{
			gunFireRate = stats.gunFireRate;
			gunDamage = stats.gunDamage;
			sprite.Modulate = stats.gunModulate; 
			GD.Print($"Fire rate: {gunFireRate}, damage: {gunDamage}");
		}
	}

	public Vector3 Shoot()
	{
		animPlayer.Play("shoot");
		audioPlayer.Stream = gunShot;
		audioPlayer.Play();
		canShoot = false;
		timer.WaitTime = gunFireRate;
		timer.Start();
		if(raycast.IsColliding() && ((Node)raycast.GetCollider()).IsInGroup("enemies"))
		{
			Enemy body = (Enemy)raycast.GetCollider();
			float result = body.damage(gunDamage);
			body.hit = true;
			if(body.health < 0 && result != 0 && body.hit)
			{
				GD.Print("Enemy");
				if(body.fireRate)
				{
					gunDamage = Mathf.Clamp(gunDamage + result, 40.0f, 120.0f);
					gunFireRate = Mathf.Clamp(result/100.0f+gunFireRate, 0.3f, 2.0f);
					sprite.Modulate -= new Color(-0.2f, 0.2f, 0.2f, 0.0f);
				}
				else
				{
					gunFireRate = Mathf.Clamp(result/100.0f-gunFireRate, 0.3f, 2.0f);
					gunDamage = Mathf.Clamp(gunDamage - result, 40.0f, 120.0f);
					sprite.Modulate -= new Color(0.2f, 0.2f, -0.2f, 0.0f);
				}
			}
			// body.hit = true;
		}
		return raycast.GetCollisionPoint();
	}

	public void SaveStats() 
	{
		if (gunStats is GunStats stats)
		{
			stats.gunFireRate = gunFireRate;
			stats.gunDamage = gunDamage; 
			stats.gunModulate = sprite.Modulate; 
			GD.Print($"Fire rate: {stats.gunFireRate}, damage: {stats.gunDamage}");
		}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(player.moveVector != new Vector3(0.0f, 0.0f, 0.0f) && !animPlayer.IsPlaying())
			animPlayer.Play("walk");
		// else if(!animPlayer.IsPlaying())
		// 	animPlayer.Stop();
	}

	public void _on_Timer_timeout()
	{
		canShoot = true;
	}
}
