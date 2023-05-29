using Godot;
using System;

public class Enemy : KinematicBody
{
	[Export]
	float speed;
	[Export]
	float health;
	
	AnimationPlayer animPlayer;
	World world;

	public override void _Ready()
	{
   		world = (World)GetParent().GetParent();
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		health = 10;
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
			animPlayer.Play("kill");
	}

	public void _on_AnimationPlayer_animation_finished(String animName)
	{
		if(animName == "kill")
			QueueFree();
	}

}
