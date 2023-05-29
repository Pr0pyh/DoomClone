using Godot;
using System;

public class Enemy : KinematicBody
{
	[Export]
	float speed;
	[Export]
	float health;
	
	World world;

	public override void _Ready()
	{
   		world = (World)GetParent().GetParent();
		health = 10;
	}

	public override void _Process(float delta)
	{
		Player player = world.GetNode<Player>("Player");
		LookAt(player.Translation, new Vector3(0.0f,1.0f,0.0f));
		Vector3 playerPosNorm = (player.Translation - GlobalTransform.origin).Normalized();
		MoveAndSlide(playerPosNorm * speed);
	}

	public void damage(float number)
	{
		health -= number;
		if(health<0)
			QueueFree();
	}

}
