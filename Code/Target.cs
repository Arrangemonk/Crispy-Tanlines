using Godot;
using System;

public class Target : MeshInstance
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Random rnd;
    private Vector3 current;
    private Vector3 target;
    private float timer = 0;
    public override void _Ready()
    {
        rnd = new Random();
        current = target = new Vector3(0.256f,0.273f,-0.051f);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        const float scale = 30;
        if (timer > scale)
        {
            timer = 0;
            current = target;
            target = new Vector3(rnd.Next(-3, 3),0.273f, rnd.Next(-3, 3));
        }

        var amount = Mathf.Min(1, Math.Max(0, timer / scale));
        //var amount = 1f;
        Transform = new Transform(Transform.basis, target * amount + current * (1f - amount));
        timer += delta;
    }
}
