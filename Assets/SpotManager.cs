using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spot に対する参照や他への参照の際に利用するために使う
/// </summary>
public class SpotManager : MonoBehaviour
{
    [SerializeField]
    public List<Spot> spot = new();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

}
