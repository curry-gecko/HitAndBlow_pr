using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{
    [SerializeField] public int SequenceRangeMax = 6; // 最大の数字
    [SerializeField] public int SequenceRangeMin = 1; // 最小の数字
    private List<int> correctSequence;
    internal List<int> playerSequence { get; private set; }
    //
    public int hit = 0;
    public int blow = 0;
    public bool isCollected = false;

    public Answer()
    {

    }

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

    public Answer CheckAnswer(List<int> _playerSequence)
    {
        Answer ret = new(correctSequence)
        {
            playerSequence = _playerSequence,
            hit = 0,
            blow = 0,
            isCollected = false,
        };
        // 桁数が違う場合
        if (_playerSequence.Count != correctSequence.Count)
        {
            return ret;
        }

        // 結果を格納する
        ret.isCollected = true;
        for (int i = 0; i < _playerSequence.Count; i++)
        {
            if (_playerSequence[i] == correctSequence[i])
            {
                ret.hit++;
            }
            else if (correctSequence.Contains(_playerSequence[i]))
            {
                ret.blow++;
                ret.isCollected = false;
            }
            else
            {
                ret.isCollected = false;
            }
        }

        return ret;
    }

    public Answer ProvideHint(List<int> playerSequence)
    {
        Answer ret = new();

        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] == correctSequence[i])
            {
                ret.hit++;
            }
            else if (correctSequence.Contains(playerSequence[i]))
            {
                ret.blow++;
            }
        }

        return ret;
    }

    public List<int> GetCorrectSequence()
    {
        return new List<int>(correctSequence);
    }
}