using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行および判定を行う｡
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] public List<Spot> Spots;
    [SerializeField] public List<Pin> Pin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    public void OnClickSubmit()
    {
        // すべてのSpotにピンが存在すること
        bool hasEmptySpot = Spots.Find(spot => spot.currentPin == null);
        Debug.Log("tag" + ":" + hasEmptySpot);
        if (hasEmptySpot)
        {
            Debug.Log("tag" + ":" + "has empty spot");
        }
        return;
    }
}
