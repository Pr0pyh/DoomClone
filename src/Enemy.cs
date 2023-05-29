using Godot;
using System;

public class Enemy : KinematicBody
{
	[Export]
	float speed;
	[Export]
	float health;
	
	public bool hit;
	AnimationPlayer animPlayer;
	World world;
	Timer t;
	Sprite3D sprite;

	public override void _Ready()
	{
   		world = (World)GetParent().GetParent();
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		sprite = GetNode<Sprite3D>("Sprite3D");
		t = GetNode<Timer>("Timer");
		health = 100;
		hit = false;
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

	public void damage(float number)
	{
		health -= number;
		if(health<0)
		{
			animPlayer.Play("kill");
		}
		else
		{
			animPlayer.Play("hurt");
			speed = 0;
			t.Start();
		}
	}

	public void _on_AnimationPlayer_animation_finished(String animName)
	{
		if(animName == "kill")
			QueueFree();
	}
	
	private void _on_Timer_timeout()
	{
		speed = 1;
	}
}
