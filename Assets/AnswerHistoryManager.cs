using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Answerの履歴を管理する
/// </summary>
public class AnswerHistoryManager : MonoBehaviour
{
    private List<Answer> playerAnswerHistory = new();
    public Answer LatestAnswer => playerAnswerHistory.Last();
    [SerializeField] private AnswerPresenter answerPresenter;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddAnswer(Answer newAnswer)
    {
        playerAnswerHistory.Add(newAnswer);
        answerPresenter.DisplayAnswer(newAnswer);

    }
}