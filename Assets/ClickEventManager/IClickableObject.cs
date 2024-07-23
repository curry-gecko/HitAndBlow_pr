using UnityEngine;

public interface IClickableObject
{
    void OnMouseClick();
    void OnMouseDragging();
    void OnMouseRelease();
    void OnMouseOnObject();
    void OnObjectOnDragged(GameObject obj);
    bool Draggable { get; }
}