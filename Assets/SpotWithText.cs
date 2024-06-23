using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スポットに追従するテキストオブジェクト(デバッグ用)
/// </summary>using UnityEngine;
using TMPro;  // TextMeshProを使用する場合

public class SpotWithText : MonoBehaviour
{
    public string spotTypeName; // TODO: ReactiveProperty の利用
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
            text.text = spotTypeName;
        }
    }
}
