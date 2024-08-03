using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カード情報に対する参照や他への参照の際に利用するために使う
/// </summary>
public class CardManager : MonoBehaviour
{
    [SerializeField]
    public List<Card> hand = new();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReleaseCard()
    {
        foreach (var item in hand)
        {
            item.SetPending(false);
        }
    }
}
