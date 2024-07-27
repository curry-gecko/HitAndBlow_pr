using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ゲームの進行および判定を行う｡
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] public List<Spot> Spots;
    [SerializeField] public List<Pin> Pin;
    [SerializeField] public AnswerPresenter answerPresenter;
    [SerializeField] public PinManager pinManager;
    // Start is called before the first frame update
    void Start()
    {
        // spot の初期化
        List<string> list = GetSuitList()
            .Select(suit => suit.GetDescription()).ToList()
            .OrderBy(_ => Guid.NewGuid()).ToList(); // 順番変更
        for (int i = 0; i < list.Count; i++)
        {
            Spots[i].typeName.Value = list[i];
        }
        // pin の初期化
        pinManager.AddPin(GetSuitList());

        // Eventの購読
        EventManager em = EventManager.Instance;
        em.OnObjectsReleased
            .Subscribe(objects => OnObjectsReleased(objects.Item1, objects.Item2))
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    public void OnClickSubmit()
    {
        // すべてのSpotにピンが存在すること
        bool hasEmptySpot = Spots.Find(spot => spot.CurrentPin.Value == null);

        Debug.Log("tag" + ":" + hasEmptySpot);
        if (hasEmptySpot)
        {
            Debug.Log("tag" + ":" + "has empty spot");
            return;
        }

        if (EvaluateGuess())
        {
            // すべて正解
            Debug.Log("tag" + ":" + "Correct Pins.");
        }
        else
        {
            //
            Debug.Log("tag" + ":" + "Any Incorrect Pins.");
        }

        answerPresenter.SetAnswer();
        return;
    }

    //初期化用途として､スートのリストを返す
    private List<SuitType> GetSuitList()
    {
        List<SuitType> list = new((SuitType[])Enum.GetValues(typeof(SuitType)));

        return list;
    }

    private bool EvaluateGuess()
    {
        bool let = false;

        let = Spots.Find(Spot => Spot.IsCollect.Value == false) == null;

        return let;
    }

    private void OnObjectsReleased(IClickableObject one, IClickableObject two)
    {
        if (one.Tag == "Card" && two.Tag == "Spot")
        {
            // 記述
            Debug.Log("Released Card to Spot!!");
        }
    }
}