using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// カードの基底クラス
/// </summary>
public class ICard : MonoBehaviour
{
    //
    public int number = 0;
    public List<Sprite> sprites;

    void Start()
    {
    }

    void Update()
    {
        // 必要に応じて処理を追加
    }

    internal void ChangeSpriteFromNumber(int number)
    {
        SpriteRenderer renderer = transform.GetComponentInChildren<SpriteRenderer>();

        if (renderer == null)
        {
            Debug.LogWarning("SpriteRenderer is not found in children of the object:" + gameObject.name + "");
            return;
        }
        if (number >= 0 && number < sprites.Count)
        {
            Debug.Log("aaa" + ":" + "Change Sprite From Number:" + number);
            renderer.sprite = sprites[number];
        }
        Debug.Log("tag" + ":" + "Change Sprite From Number");
    }

}
