using Godot;
using System;

public class final : Control
{
    [Export]
    PackedScene menu;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        menu = (PackedScene)ResourceLoader.Load("res://src/levels/Menu.tscn");
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

    public void _on_Button_pressed()
    {
        GetTree().Quit();
    }
}
