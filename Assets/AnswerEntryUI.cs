using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerEntryUI : MonoBehaviour
{


    [SerializeField] private TMP_Text playerSequenceText;
    [SerializeField] private TMP_Text hitText;
    [SerializeField] private TMP_Text blowText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI(Answer answer)
    {
        playerSequenceText.text = "Player: " + string.Join(", ", answer.playerSequence);
        hitText.text = "Hit: " + answer.hit;
        blowText.text = "Blow: " + answer.blow;
    }
}
