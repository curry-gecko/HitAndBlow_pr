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
    private readonly ReactiveProperty<Pin> currentPin = new();
    public IReadOnlyReactiveProperty<Pin> CurrentPin => currentPin;

    public bool Draggable => false;
    public GameObject Me => gameObject;

    public string Tag => "Spot";

    //
    public PinType pinType;
    public bool isEmptyObject => CurrentPendingCard == null;
    private Card CurrentPendingCard = null;

    //

    [SerializeField] SpotWithAnswer answer;

    // Start is called before the first frame update
    void Start()
    {
        typeName.Subscribe(name => { pinType = new PinType(name); })
            .AddTo(this);

        currentPin.Subscribe(pin => isCollect.Value = pin != null && pin.TypeName.Value == typeName.Value)
            .AddTo(this);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Pin pin = other.GetComponent<Pin>();

        if (pin != null && currentPin.Value == null)
        {
            pin.SetSpot(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Pin pin = other.GetComponent<Pin>();

        if (pin != null)
        {
            pin.ClearSpot();
        }
    }

    public void SetCurrentPin(Pin pin)
    {
        currentPin.Value = pin;
    }

    public void SetCard(Card card)
    {
        // カードの親に自身をセット
        CurrentPendingCard = card;
    }

    public void RemoveCard()
    {
        CurrentPendingCard = null;
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
