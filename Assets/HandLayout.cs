using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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

    private Vector3 CardLocalPosition = new(0, 3.5f, -1); // TODO 定数

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

            if (_card.IsDragging.Value)
            {

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
                // ドラッグ中は常に対象を追尾するTweenで上書きする
                if (_card.CurrentPositionTween != null && _card.CurrentPositionTween.IsActive())
                {
                    _card.CurrentPositionTween.Kill();
                }
                _card.CurrentPositionTween = _card.transform.DOMove(newPos, 0.1f)
                        .OnComplete(() => _card.CurrentPositionTween = null);
            }
            else if (_card.IsPending.Value)
            {
                /// Spot に Pending されている場合､規定のローカル座標で上書きする
                _card.transform.localPosition = CardLocalPosition;
            }
            else if (_card.CurrentPositionTween == null && !_card.CurrentPositionTween.IsActive() && !_card.IsPending.Value)
            {
                // 手札に存在する状態
                float xPosition = i * xPadding;
                float yPosition = _card.IsMouseOnObject.Value ? yPadding : 0;
                float zPosition = _card.IsMouseOnObject.Value ? 0 : i * zPadding + 1;
                Vector3 pos = new Vector3(xPosition, yPosition, zPosition);
                _card.CurrentPositionTween = _card.transform.DOLocalMove(pos, 0.1f)
                        .OnComplete(() => _card.CurrentPositionTween = null);
            }
        }
    }
}
