using Godot;
using System;

public class end : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void _on_AudioStreamPlayer3D_finished()
    {
        PackedScene nextLevel = (PackedScene)ResourceLoader.Load("res://src/levels/new scene.tscn");
        GetTree().ChangeSceneTo(nextLevel);
    }

    public void _on_Timer_timeout()
    {
        PackedScene nextLevel = (PackedScene)ResourceLoader.Load("res://src/levels/new scene.tscn");
        GetTree().ChangeSceneTo(nextLevel);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
