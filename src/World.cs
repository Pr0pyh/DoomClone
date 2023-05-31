using Godot;
using System;

public class World : Spatial
{
	[Export]
	public int number;
	[Export]
	PackedScene nextLevel;
	Sprite3D cube;
	Player player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<Player>("Player");
	}

	public void LowerNumber()
	{
		number--;
		if(number <= 0)
		{
			player.Gun.SaveStats();
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
