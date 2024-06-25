using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        List<string> list = GetSuitList()
            .Select(suit => suit.GetDescription()).ToList()
            .OrderBy(_ => Guid.NewGuid()).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            Spots[i].typeName.Value = list[i];
        }
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

    //初期化用途として､スートのリストを返す
    private List<SuitType> GetSuitList()
    {
        List<SuitType> list = new((SuitType[])Enum.GetValues(typeof(SuitType)));

        return list;
    }
}
