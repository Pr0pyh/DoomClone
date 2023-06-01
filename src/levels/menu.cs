using Godot;
using System;

public class menu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    PackedScene level;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        level = (PackedScene)ResourceLoader.Load("res://src/levels/Level1.tscn");
    }

    public void _on_Button_pressed()
    {
        GetTree().ChangeSceneTo(level);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
