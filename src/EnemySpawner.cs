using Godot;
using System;

public class EnemySpawner : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public bool fireRate;
	[Export]
	PackedScene enemyScene;
	Enemy enemy;
	Timer timer;
	Spatial container;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		container = GetParent().GetNode<Spatial>("Enemies");
	}

	public void _on_Timer_timeout()
	{
		enemy = (Enemy)enemyScene.Instance();
		if(fireRate)
			enemy.fireRate = true;
		enemy.Translation = Translation;
		enemy.Scale = new Vector3(1.5f, 1.5f, 1.0f);
		container.AddChild(enemy);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
