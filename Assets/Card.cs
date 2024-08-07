using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// カードクラス
/// 初期化時にスプライトを指定する
/// 処理について､transform に対する移動などのみを実装する｡ロジックを持たせない
/// </summary>
public class Card : ICard, IClickableObject
{

    // Status
    private ReactiveProperty<bool> isDragging = new();
    public IReadOnlyReactiveProperty<bool> IsDragging => isDragging;
    private ReactiveProperty<bool> isMouseOnObject = new();
    public IReadOnlyReactiveProperty<bool> IsMouseOnObject => isMouseOnObject;
    private ReactiveProperty<bool> isPending = new();
    public IReadOnlyReactiveProperty<bool> IsPending => isPending;

    //
    public bool Draggable { get => true; } // TODO drag可能かどうかを判定する
    public GameObject Me => gameObject;
    public string Tag => "Card";


    // Transform 系
    private Vector3 originalScale;
    private float zoomScale = 1.2f;
    private float duration = 0.1f;
    private Tween scaleTween = null;
    public Tween CurrentPositionTween = null;

    void Start()
    {
        // マウスリリース
        this.OnMouseUpAsObservable()
            .Where(_ => IsDragging.Value)
            .Subscribe(_ => OnMouseRelease())
            .AddTo(this);

        // 
        originalScale = transform.localScale;

        // number に応じたSpriteを設定する
        ChangeSpriteFromNumber(number);
    }

    void Update()
    {
        // 必要に応じて処理を追加
    }

    public void OnMouseClick()
    {
        isDragging.Value = true;
        if (scaleTween != null && scaleTween.IsActive())
        {
            scaleTween.Complete();
        }
        //
        SetPending(false);
        // AddNumber(1);
    }

    public void OnMouseDragging()
    {
        // ドラッグ処理
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = newPos;
    }

    public void OnMouseRelease()
    {
        isDragging.Value = false;
        // マウスアップ処理
    }

    public void OnMouseOnObject()
    {
        // 
        if (isMouseOnObject.Value) return;
        if (isDragging.Value) return;

        scaleTween ??= transform.DOScale(originalScale * zoomScale, duration).SetEase(Ease.InOutQuart);
        isMouseOnObject.Value = true;

        this.OnMouseExitAsObservable()
            .First().Subscribe(_ =>
            {
                isMouseOnObject.Value = false;
                scaleTween = transform.DOScale(originalScale, duration)
                        .SetEase(Ease.InOutQuart)
                        .OnComplete(() => scaleTween = null);

            })
            .AddTo(this);
    }

    public void SetPending(bool pending)
    {
        isPending.Value = pending;
    }
}
