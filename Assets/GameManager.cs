using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// ゲームの進行および判定を行う｡
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] public AnswerPresenter answerPresenter;
    [SerializeField] public AnswerHistoryManager answerHistoryManager;
    [SerializeField] public CardManager cardManager;
    [SerializeField] public SpotManager spotManager;


    //
    private Vector3 CardLocalPosition = new(0, 3.5f, -1); // TODO 定数

    //
    [SerializeField] public int numberOfDigits = 4;
    private Answer answer = null;
    // Start is called before the first frame update
    void Start()
    {
        // Eventの購読
        EventManager em = EventManager.Instance;
        em.OnObjectsReleased
            .Subscribe(objects => OnObjectsReleased(objects.Item1, objects.Item2))
            .AddTo(this);

        // answer の初期化
        answer = new Answer(numberOfDigits, false);
#if UNITY_EDITOR
        Debug.Log("The answer is :" + string.Join(",", answer.GetCorrectSequence()));
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    public void OnClickSubmit()
    {
        List<int> playerSequence = spotManager.GetPlayerSequence();

        Debug.Log("" + string.Join(",", playerSequence));

        var result = answer.CheckAnswer(playerSequence);

        if (result.isCollected)
        {
            // すべて正解
            Debug.Log("tag" + ":" + "Correct Pins.");
        }
        else
        {
            //
            Debug.Log("tag" + ":" + "Any Incorrect Pins. hit:" + result.hit + ", blow:" + result.blow);
        }

        answerPresenter.DisplayResult();
        return;
    }

    private void OnObjectsReleased(IClickableObject one, IClickableObject two)
    {
        if (one.Tag == "Card" && two.Tag == "Spot")
        {
            // 記述
            if (one.Me.TryGetComponent<Card>(out var card) && two.Me.TryGetComponent<Spot>(out var spot))
            {
                if (!spot.isEmptyObject.Value) { return; }
                spot.SetCard(card);
                // Card の親に Spotをセット
                card.transform.parent = spot.transform;
                card.transform.localPosition = CardLocalPosition;
                card.SetPending(true);
                card.IsPending
                    .Where(p => !p)
                    .First()
                    .Subscribe(_ =>
                    {
                        card.transform.parent = cardManager.transform;
                        spot.RemoveCard();
                    }).AddTo(this);
            }
        }
    }
}