using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class Spot : MonoBehaviour
{
    //
    public ReactiveProperty<string> typeName = new();
    private readonly ReactiveProperty<bool> isCollect = new();
    public ReactiveProperty<bool> IsCollect => isCollect;
    private readonly ReactiveProperty<Pin> currentPin = new();
    public IReadOnlyReactiveProperty<Pin> CurrentPin => currentPin;
    //
    public PinType pinType;
    [SerializeField] SpotWithAnswer answer;

    // Start is called before the first frame update
    void Start()
    {
        typeName.Subscribe(name => { pinType = new PinType(name); })
            .AddTo(this);

        currentPin.Subscribe(pin => isCollect.Value = pin != null && pin.typeName.Value == typeName.Value)
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
}
