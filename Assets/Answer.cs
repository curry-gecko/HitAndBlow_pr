using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Answer
{
    private readonly List<SuitType> correctSequence;
    public int hit = 0;
    public int blow = 0;

    public Answer(List<SuitType> sequence)
    {
        correctSequence = new List<SuitType>(sequence);
    }

    public bool CheckAnswer(List<SuitType> playerSequence)
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

    public Answer ProvideHint(List<SuitType> playerSequence)
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

    public List<SuitType> GetCorrectSequence()
    {
        return new List<SuitType>(correctSequence);
    }
}