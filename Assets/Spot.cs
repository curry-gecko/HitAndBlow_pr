using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class Spot : MonoBehaviour, IClickableObject
{
    //
    public ReactiveProperty<string> typeName = new();
    private readonly ReactiveProperty<bool> isCollect = new();
    public ReactiveProperty<bool> IsCollect => isCollect;

    public bool Draggable => false;
    public GameObject Me => gameObject;

    public string Tag => "Spot";

    //
    public ReactiveProperty<bool> isEmptyObject = new(true);
    private Card CurrentPendingCard = null;

    //


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetCard(Card card)
    {
        // カードの親に自身をセット
        CurrentPendingCard = card;
        isEmptyObject.Value = false;
    }

    public void RemoveCard()
    {
        CurrentPendingCard = null;
        isEmptyObject.Value = true;
    }

    public void OnMouseClick()
    {
        return;
    }

    public void OnMouseDragging()
    {
        return;
    }

    public void OnMouseRelease()
    {
        return;
    }

    public void OnMouseOnObject()
    {
        return;
    }
}
