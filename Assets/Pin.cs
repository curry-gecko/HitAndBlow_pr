using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class Pin : MonoBehaviour, IClickableObject
{
    private Vector3 offset;
    private bool isDragging = false;
    private Spot currentSpot = null;

    void Start()
    {
        // マウスダウンイベントの設定
        // this.OnMouseDownAsObservable()
        //     .Subscribe(_ => OnMouseDown());

        // マウスドラッグイベントの設定
        this.UpdateAsObservable()
            .Where(_ => isDragging)
            .Subscribe(_ => OnMouseDrag());

        // マウスアップイベントの設定
        this.OnMouseUpAsObservable()
            .Subscribe(_ => OnMouseUp());
    }

    public void OnMouseDown()
    {
        isDragging = true;
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
        }
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