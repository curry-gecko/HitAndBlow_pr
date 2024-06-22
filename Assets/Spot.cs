using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public Pin currentPin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Pin pin = other.GetComponent<Pin>();

        if (pin != null)
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
