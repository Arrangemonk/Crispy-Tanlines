using Godot;
using System;
using System.Threading;
using Godot.Collections;
using Array = Godot.Collections.Array;

public class Draggable : Node
{
    [Signal]
    public delegate void DragStart(Node node);

    [Signal]
    public delegate void DragStop(Node node);

    [Signal]
    public delegate void DragMove(Node node,Dictionary cast);

    public int Bit = 19;
    private uint areaLayer;
    private uint areaMask;
    private Vector2 dragOffset;
    private Node hovered;
    private Node current;
    private DragDropContainer dragDropContainer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        areaLayer = GetParent<RigidBody>().CollisionLayer;
        areaMask = GetParent<RigidBody>().CollisionMask;
        if (Engine.EditorHint)
        {
            SetProcess(false);
            return;
        }

        dragDropContainer = GetTree().Root.GetNode<DragDropContainer>(nameof(DragDropContainer));
        if (dragDropContainer == null)
            GD.PrintErr("Missing DragDropController singletron!");
        else
        {
            dragDropContainer.RegisterDraggable(this);
            var draggable = GetParent<CollisionObject>();
            var parent = new Godot.Collections.Array(draggable);
            draggable.Connect("mouse_entered", this, nameof(MouseEntered), parent);
            draggable.Connect("mouse_exited", this,nameof(MouseExited), parent);
            //draggable.Connect("input_event", this, nameof(_InputEvent), parent);
        }

    }

    private string GetConfigurationWarning()
    {
        return GetParent() is CollisionObject ? "" : "Not under a collision object";
    }

    public void OnHover(Dictionary cast)
    {
        EmitSignal(nameof(DragMove), this, cast);
        (GetParent<CollisionObject>() as RigidBody)?._on_Draggable_DragMove(this,cast);
    }

    private void MouseEntered(Node node)
    {
        hovered = node;
    }

    private void MouseExited(Node node)
    {
        hovered = null;
    }

    public void _InputEvent(System.Object camera, InputEvent @event, Vector3 position, Vector3 normal, int shapeIdx)
    {
        var mouseButtonEvent = @event as InputEventMouseButton;
        if (mouseButtonEvent == null || mouseButtonEvent.ButtonIndex != (int)ButtonList.Left) return;
        if (!mouseButtonEvent.IsPressed()) return;
        if (hovered != null)
        {
            current = hovered.GetParent();
            EmitSignal(nameof(DragStart), this);
        }
        else if (current != null)
        {
            EmitSignal(nameof(DragStop), this);
        }
    }
}
