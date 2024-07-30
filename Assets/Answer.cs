using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{
    [SerializeField] public int SequenceRangeMax = 6; // 最大の数字
    [SerializeField] public int SequenceRangeMin = 1; // 最小の数字
    private readonly List<int> correctSequence;
    public int hit = 0;
    public int blow = 0;


    public Answer(List<int> sequence)
    {
        correctSequence = new List<int>(sequence);
    }

    public Answer(int numberOfDigits, bool canDuplication = false)
    {
        correctSequence = GenerateSequence(numberOfDigits, canDuplication);
    }

    // ランダムなシーケンスを生成する
    private List<int> GenerateSequence(int numberOfDigits, bool canDuplication)
    {
        List<int> ret = new();
        List<int> availableDigits = new List<int>();
        for (int i = 1; i < SequenceRangeMax + 1; i++)
        {
            availableDigits.Add(i);
        }

        for (int i = 0; i < numberOfDigits; i++)
        {
            int index = Random.Range(0, availableDigits.Count);
            ret.Add(availableDigits[index]);
            if (!canDuplication)
            {
                // 重複許可しない場合は､使用した数字を削除
                availableDigits.RemoveAt(index);
            }
        }

        return ret;
    }

    public bool CheckAnswer(List<int> playerSequence)
    {
        if (playerSequence.Count != correctSequence.Count)
            return false;

        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }

        return true;
    }

    public Answer ProvideHint(List<int> playerSequence)
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] == correctSequence[i])
            {
                hit++;
            }
            else if (correctSequence.Contains(playerSequence[i]))
            {
                blow++;
            }
        }

        return this;
    }

    public List<int> GetCorrectSequence()
    {
        return new List<int>(correctSequence);
    }
}