using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerEntryUI : MonoBehaviour
{


    // [SerializeField] private TMP_Text playerSequenceText;
    [SerializeField] private TMP_Text hitText;
    [SerializeField] private TMP_Text blowText;

    //
    [SerializeField] private GameObject playerSequence;
    [SerializeField] private List<ICard> iCards = new();

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
        //playerSequenceText.text = "" + string.Join(", ", answer.playerSequence);
        foreach (var item in answer.playerSequence.Select((value, index) => new { value, index }))
        {
            Debug.Log("index" + ":" + item.index);
            Debug.Log("value" + ":" + item.value);
            iCards[item.index].ChangeSpriteFromNumber(item.value);
        }
        hitText.text = "" + answer.hit;
        blowText.text = "" + answer.blow;
    }
}
