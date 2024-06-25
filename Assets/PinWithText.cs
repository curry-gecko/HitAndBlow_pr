using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ピンに追従するテキストオブジェクト(デバッグ用)
/// </summary>using UnityEngine;
using TMPro;
using UniRx;
using Unity.VisualScripting;  // TextMeshProを使用する場合

public class PinWithText : MonoBehaviour
{
    public string pinTypeName; // TODO: ReactiveProperty の利用
    public Pin pin;
    public TextMeshProUGUI text;

    private void Start()
    {
        // テキストを更新
        if (pin != null)
        {
            pin.typeName.Subscribe(name => UpdateText(name)).AddTo(this);
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
