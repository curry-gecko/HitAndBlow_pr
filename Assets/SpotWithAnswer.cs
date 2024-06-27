using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UniRx;
using System;  // TextMeshProを使用する場合

/// <summary>
/// スポットに追従するテキストオブジェクト(正解表示用)
/// </summary>using UnityEngine;
public class SpotWithAnswer : MonoBehaviour
{
    public string spotTypeName; // TODO: ReactiveProperty の利用
    public Spot spot;
    public TextMeshProUGUI text;

    private void Start()
    {
        // テキストをピンオブジェクトの位置に追従させる
        if (TryGetComponent<Collider2D>(out var collider))
        {
            // スポットのバウンディングボックスのサイズを取得
            float spotHeight = collider.bounds.size.y;
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.y += spotHeight * 50;
            Debug.Log("" + spotHeight);
            text.transform.position = pos;
        }
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {

        if (text != null)
        {
            text.text = spot.IsCollect.ToString();
        }
    }
}
