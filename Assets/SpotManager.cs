using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Spot に対する参照や他への参照の際に利用するために使う
/// </summary>
public class SpotManager : MonoBehaviour
{
    [SerializeField]
    public List<Spot> spots = new();

    private IObservable<bool> hasEmptyObjectStream;
    public IObservable<bool> HasEmptyObjectStream
    {
        get
        {
            if (hasEmptyObjectStream == null)
            {
                hasEmptyObjectStream = spots
                    .Select(sp => sp.isEmptyObject)
                    .CombineLatest()
                    .Select(isEmpty => isEmpty.All(b => !b))
                    .Replay(1)
                    .RefCount();
            }
            return hasEmptyObjectStream;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpot(spots);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpot(List<Spot> newSpots)
    {
        spots = newSpots;
        hasEmptyObjectStream = null;
        var lazy = HasEmptyObjectStream; //初期化

    }
}
