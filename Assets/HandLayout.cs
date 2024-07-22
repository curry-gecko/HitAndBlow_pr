using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// カードを並べる処理を扱う
/// カード単体ではなく､複数または複数に影響を与える場合の処理を記述する
/// </summary>
public class HandLayout : MonoBehaviour
{
    [SerializeField]
    private CardManager manager; // DI 予定
    private List<Card> cardPosition = new();
    private float xPadding = 2.5f;
    private float yPadding = 0.2f;
    private float zPadding = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (manager != null)
        {
            foreach (var item in manager.hand)
            {
                cardPosition.Add(item);
            };

        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHandLayout();
    }
    // 手札をデッキから追加する
    public void AddCard(Card card)
    {

    }

    // レイアウト更新
    private void UpdateHandLayout()
    {
        //
        for (int i = 0; i < cardPosition.Count; i++)
        {
            Card _card = cardPosition[i];
            float xPosition = i * xPadding;
            float yPosition = _card.IsMouseOnObject.Value ? yPadding : 0;
            float zPosition = _card.IsMouseOnObject.Value ? 0 : i * zPadding + 1;


            if (!_card.IsDragging.Value)
            {
                _card.transform.localPosition = new(xPosition, yPosition, zPosition);
            }
        }
    }
}
