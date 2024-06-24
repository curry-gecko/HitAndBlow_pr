using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEditor.PackageManager;
using System;
using DG.Tweening;

public class Pin : MonoBehaviour, IClickableObject
{
    private Vector3 offset;
    private bool isDragging = false;
    private Spot currentSpot = null;
    private ReactiveProperty<int> statusCode = new(); // 仮 
    private SpriteRenderer spriteRenderer = null;

    void Start()
    {
        // マウスダウンイベントの設定
        // this.OnMouseDownAsObservable()
        //     .Subscribe(_ => OnMouseDown());

        // マウスドラッグイベントの設定
        this.UpdateAsObservable()
            .Where(_ => isDragging)
            .Subscribe(_ => OnMouseDrag())
            .AddTo(this);

        if (TryGetComponent<SpriteRenderer>(out spriteRenderer))
        {
            statusCode.Subscribe(code => OnChangedStatusCode(code)).AddTo(this);
        }


        // マウスアップイベントの設定
        this.OnMouseUpAsObservable()
            .Subscribe(_ => OnMouseUp())
            .AddTo(this);
    }

    private void OnChangedStatusCode(int code)
    {
        // issue: 状態の優先度が確立されていない
        spriteRenderer.color = code switch
        {
            // ドラッグ状態
            // 2 => Color.red,
            // 選択状態
            1 => Color.green,
            // 無選択状態
            _ => Color.white,
        };
    }

    public void OnMouseDown()
    {
        isDragging = true;
        // statusCode.Value = 2;

        transform.DOKill(true);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void OnMouseDrag()
    {
        if (!isDragging) return; // 冗長ではある

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos) + offset;
        transform.position = newPos;
    }

    public void OnMouseUp()
    {
        isDragging = false;

        if (currentSpot != null)
        {
            transform.SetParent(currentSpot.transform);
            currentSpot.SetCurrentPin(this);

            // セットされたスポットの中心にsnap する
            Vector3 pos = currentSpot.transform.position;
            pos.y += transform.localScale.y / 2;
            transform.DOMove(pos, 0.1f);
        }
        statusCode.Value = (currentSpot == null) ? 0 : 1;

    }

    public void SetSpot(Spot spot)
    {
        currentSpot = spot;
    }

    public void ClearSpot()
    {
        if (currentSpot != null && currentSpot.currentPin == this)
        {
            currentSpot.SetCurrentPin(null);
        }
        transform.SetParent(null);
        currentSpot = null;
    }
}