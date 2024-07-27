using UnityEngine;

public interface IClickableObject
{
    void OnMouseClick();
    void OnMouseDragging();
    void OnMouseRelease();
    void OnMouseOnObject();
    bool Draggable { get; }
    GameObject Me { get; }
}