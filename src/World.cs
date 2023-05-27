using Godot;
using System;

public class World : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Sprite3D cube;
    Player player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cube = GetNode<Sprite3D>("Sprite3D");
        player = GetNode<Player>("Player");
    }

    public void Spawn(Vector3 position)
    {
        cube.Translation = position;
        cube.LookAt(player.Translation, cube.Translation);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
