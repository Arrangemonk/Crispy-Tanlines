using Godot;
using System;
using Godot.Collections;
using Object = Godot.Object;

public class RigidBody : Godot.RigidBody
{
    private float timer = 0;
    private float gravity = -0.5f;
    private void LookFollow(PhysicsDirectBodyState state, Transform currentTransform, Vector3 targetPosition)
    {

        var velocity = (targetPosition * new Vector3(1, 0, 1) - currentTransform.origin * new Vector3(1, 0, 1));
        if (velocity.Length() > 1.0)
            velocity = velocity.Normalized();

        state.LinearVelocity = velocity * (0.5f - Mathf.Sin(timer) * 0.5f);
        SetPhysicsProcess(false);
    }

    public override void _IntegrateForces(PhysicsDirectBodyState state)
    {
        var targetPosition = GetNode<MeshInstance>("..//Target").GlobalTransform.origin;
        LookFollow(state, GlobalTransform, targetPosition);
    }

    public void _on_Draggable_DragMove(Node node, Dictionary cast)
    {
        foreach(var key in cast.Keys)
            GD.Print(key);
        Translation = (Vector3)cast["position"];
    }

    private Draggable draggable;
    public override void _Ready()
    {
        draggable = GetNode<Draggable>(nameof(Draggable));
        gravity = -0.5f;
    }

    public override void _Process(float delta)
    {
        timer += delta;
        gravity += (delta * 0.5f);
        GravityScale = Mathf.Max(0.0f,Mathf.Min(1.0f,gravity));
        if (timer > Mathf.Pi)
        {
            timer = 0;
        }
    }

    public override void _InputEvent(Object camera, InputEvent @event, Vector3 position, Vector3 normal, int shapeIdx)
    {
      draggable._InputEvent(camera,@event,position,normal,shapeIdx);
    }

}


