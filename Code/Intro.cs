using Godot;
using System;

public class Intro : Spatial
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetPhysicsProcess(false);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
}
