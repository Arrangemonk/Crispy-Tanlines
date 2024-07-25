using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public class DragDropContainer : Node
{
    [Export] 
    public float RayLength { get; set; } = 100;

    private List<Draggable> draggables;
    private Camera camera;
    private Draggable dragging;

    public override void _Ready()
    {
        draggables = new List<Draggable>();
        camera = GetTree().Root.GetCamera();
        SetPhysicsProcess(false);
        GD.Print("boobs");
    }

    public void RegisterDraggable(Draggable node)
    {
        draggables.Add(node);
        node.Connect(nameof(Draggable.DragStart), this, nameof(DragStart));
        node.Connect(nameof(Draggable.DragStop), this, nameof(DragStop));
        GD.Print("boobs");
    }

    private void DragStart(Draggable node)
    {
        dragging = node;
        SetPhysicsProcess(true);
        GD.Print("boobs");
    }

    private void DragStop(Draggable node)
    {
        SetPhysicsProcess(false);
        GD.Print("boobs");
    }

    public override void _PhysicsProcess(float delta)
    {
        var mouse = GetViewport().GetMousePosition();
        var from = camera.ProjectRayOrigin(mouse);
        var to = from + camera.ProjectLocalRayNormal(mouse) * RayLength;

        var cast = camera.GetWorld().DirectSpaceState.IntersectRay(from, to,
            new Godot.Collections.Array { dragging.GetParent() }, dragging.GetParent<RigidBody>().CollisionMask, true,
            true);
        if (cast != null)
        {
            dragging.OnHover(cast);
        }
    }
}
