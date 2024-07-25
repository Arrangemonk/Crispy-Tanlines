using Godot;
using System;

public class StartButton : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Spatial gameplay;
    private Control menu;
    public override void _Ready()
    {
        PrintTreePretty();
        gameplay = GetTree().Root.GetNode<Spatial>("Intro/Gameplay");
        menu = GetTree().Root.GetNode<Control>("Intro/Menu");
        this.Connect("pressed",this, "ButtonPressed");
    }

    private void ButtonPressed()
    {
        gameplay.Visible = true;
        menu.Visible = false;
        SetPhysicsProcess(true);
    }
}
