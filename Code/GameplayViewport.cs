using Godot;
using System;
using System.Linq;

public class GameplayViewport : Viewport
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetTree().Root.Connect("size_changed", this, "_on_viewport_size_changed");
        _on_viewport_size_changed();
    }

    private void _on_viewport_size_changed()
    {
        Size = OS.WindowSize;
    }

}
