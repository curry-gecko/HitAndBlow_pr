using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スポットに追従するテキストオブジェクト(デバッグ用)
/// </summary>using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UniRx;
using System;  // TextMeshProを使用する場合

public class SpotWithText : MonoBehaviour
{
    public string spotTypeName; // TODO: ReactiveProperty の利用
    public Spot spot;
    public TextMeshProUGUI text;

    private void Start()
    {
        // テキストを更新
        if (spot != null)
        {
            spot.typeName.Subscribe(TypeName => { UpdateText(TypeName); }).AddTo(this);
        }
    }

    private void Update()
    {
        // テキストをピンオブジェクトの位置に追従させる
        text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void UpdateText(string typeName)
    {

        if (text != null)
        {
            text.text = typeName;
        }
    }
}
