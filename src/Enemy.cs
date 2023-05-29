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

	public override void _Ready()
	{
   		world = (World)GetParent().GetParent();
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
			
		if (hit)
		{	
			speed = 0;
			hit = false;
			t.Start();
			Sprite3D sprite = GetNode<Sprite3D>("Sprite3D");
			sprite.Modulate = new Color(255.0f, 0.0f, 0.0f, 1.0f);
		}
			
		MoveAndSlide(playerPosNorm * speed);
	}

	public void damage(float number)
	{
		health -= number;
		if(health<0)
			animPlayer.Play("kill");
	}

	public void _on_AnimationPlayer_animation_finished(String animName)
	{
		if(animName == "kill")
			QueueFree();
	}
	
	private void _on_Timer_timeout()
	{
		speed = 1;	
		Sprite3D sprite = GetNode<Sprite3D>("Sprite3D");
		sprite.Modulate = new Color(255.0f, 255.0f, 255.0f, 1.0f);
	}
}
