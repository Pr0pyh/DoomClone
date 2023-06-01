using Godot;
using System;

public class World : Spatial
{
	[Export]
	public int number;
	[Export]
	public PackedScene nextLevel;
	[Export]
	bool musicPlaying;
	Sprite3D cube;
	Player player;
	AnimationPlayer animPlayer;
	AudioStreamPlayer musicPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<Player>("Player");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		musicPlayer = GetNode<AudioStreamPlayer>("/root/MusicController");
		if(!musicPlaying)
			musicPlayer.Stop();
		animPlayer.Play("enter");
	}

	public void LowerNumber()
	{
		number--;
		if(number <= 0)
		{
			player.Gun.SaveStats();
			animPlayer.Play("exit");
		}
			
	}

	public void _on_AnimationPlayer_animation_finished(String animName)
	{
		if(animName == "exit")
		{
			GetTree().ChangeSceneTo(nextLevel);
		}
	}
	// public void Spawn(Vector3 position)
	// {
	// 	cube.Translation = position;
	// 	cube.LookAt(player.Translation, cube.Translation);
	// }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
