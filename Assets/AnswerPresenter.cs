using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPresenter : MonoBehaviour
{
    public List<Spot> Spots;
    public List<GameObject> answerHistory = new();

    [SerializeField] private GameObject answerEntryPrefab;
    [SerializeField] private Transform DisplayTransform;
    [SerializeField] Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 判定表示演出
    public void DisplayResult()
    {

    }

    public void DisplayAnswer(Answer answer)
    {
        // Create a new entry from the prefab
        GameObject entry = Instantiate(answerEntryPrefab, canvas.transform);

        // Assuming answerEntryPrefab has a script to update its UI

        if (entry.TryGetComponent<AnswerEntryUI>(out var entryUI))
        {
            entryUI.UpdateUI(answer);
        }
    }
}
