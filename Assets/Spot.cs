using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public ReactiveProperty<string> typeName = new();
    public PinType pinType;
    public Pin currentPin;
    public bool IsCollect => currentPin != null && currentPin.typeName.Value == typeName.Value;
    [SerializeField] SpotWithAnswer answer;

    // Start is called before the first frame update
    void Start()
    {
        typeName.Subscribe(name => { pinType = new PinType(name); })
        .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Pin pin = other.GetComponent<Pin>();

        if (pin != null && currentPin == null)
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
        currentPin = pin;
    }
}
