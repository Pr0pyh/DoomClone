using Godot;
using System;

public class Enemy : KinematicBody
{
	[Export]
	float speed;
	[Export]
	public float health;
	float gunBonus;
	
	public bool hit;
	[Export]
	public bool fireRate;
	AnimationPlayer animPlayer;
	World world;
	Timer t;
	AudioStreamPlayer3D audioPlayer;
	AudioStream killSound;
	AudioStream hurtSound;
	Sprite3D sprite;

	public override void _Ready()
	{
   		world = (World)GetParent().GetParent();
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		sprite = GetNode<Sprite3D>("Sprite3D");
		t = GetNode<Timer>("Timer");
		audioPlayer = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");
		hurtSound = (AudioStream)ResourceLoader.Load("res://assets/music/dspopain.wav");
		killSound = (AudioStream)ResourceLoader.Load("res://assets/music/dsbgdth1.wav");
		health = 100.0f;
		hit = false;
		gunBonus = 0.5f;
		if(fireRate)
			sprite.Modulate = new Color(255.0f, 0.0f, 0.0f, 1.0f);
	}

	public override void _Process(float delta)
	{
		Player player = world.GetNode<Player>("Player");
		LookAt(player.Translation, new Vector3(0.0f,1.0f,0.0f));
		Vector3 playerPosNorm = (player.Translation - GlobalTransform.origin).Normalized();
		if(!animPlayer.IsPlaying())
			animPlayer.Play("walk");
			
		MoveAndSlide(playerPosNorm * speed);
	}

	public float damage(float number)
	{
		if(hit == false)
		{
			health -= number;
			hit = true;

			if(health<0)
			{
				animPlayer.Play("kill");
				audioPlayer.Stream = killSound;
				audioPlayer.Play();
				return gunBonus;
			}
			else
			{
				animPlayer.Play("hurt");
				audioPlayer.Stream = hurtSound;
				audioPlayer.Play();
				speed = 0;
				t.Start();
				return 0;
			}
		}
		else
		{
			return 0;
		}
	}

	public void _on_AnimationPlayer_animation_finished(String animName)
	{
		if(animName == "kill")
		{
			QueueFree();
			world.LowerNumber();
		}
	}
	
	private void _on_Timer_timeout()
	{
		speed = 1;
		hit = false;
	}

	public void _on_Area_body_entered(Node body)
	{
		if(body.GetType() == typeof(Player))
		{
			Player player = (Player)body;
			player.damage(10.0f);
		}
	}
}
