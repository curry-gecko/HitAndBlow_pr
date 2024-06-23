using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ピンに追従するテキストオブジェクト
/// </summary>using UnityEngine;
using TMPro;  // TextMeshProを使用する場合

public class PinWithText : MonoBehaviour
{
    public string pinTypeName; // TODO: ReactiveProperty の利用
    public TextMeshProUGUI text;

    private void Start()
    {
        // テキストを更新
        UpdateText();
    }

    private void Update()
    {
        // テキストをピンオブジェクトの位置に追従させる
        text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void UpdateText()
    {
        if (text != null)
        {
            text.text = pinTypeName;
        }
    }
}
