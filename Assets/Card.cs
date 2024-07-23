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
public class Card : MonoBehaviour, IClickableObject
{

    private ReactiveProperty<bool> isDragging = new();
    public IReadOnlyReactiveProperty<bool> IsDragging => isDragging;
    private ReactiveProperty<bool> isMouseOnObject = new();
    public IReadOnlyReactiveProperty<bool> IsMouseOnObject => isMouseOnObject;

    public bool Draggable { get => true; } // TODO drag可能かどうかを判定する

    private ReactiveProperty<int> number = new ReactiveProperty<int>(0);
    private int maxNumber;
    private int minNumber = 0;
    [SerializeField]
    public List<Sprite> sprites;
    //
    private Vector3 originalScale;
    private float zoomScale = 1.2f;
    private float duration = 0.1f;
    private Tween scaleTween = null;
    public Tween CurrentPositionTween = null;

    void Start()
    {
        // 番号変更イベントの購読
        number.Subscribe(x => ChangeSpriteFromNumber(x))
            .AddTo(this);

        // マウスドラッグイベントの設定
        // this.UpdateAsObservable()
        //     .Where(_ => IsDragging.Value)
        //     .Subscribe(_ => OnMouseDragging())
        //     .AddTo(this);

        // マウスリリース
        this.OnMouseUpAsObservable()
            .Where(_ => IsDragging.Value)
            .Subscribe(_ => OnMouseRelease())
            .AddTo(this);

        // 最大番号をスプライト数から設定
        maxNumber = sprites.Count - 1;

        // 
        originalScale = transform.localScale;
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

    private void AddNumber(int _number)
    {
        int x = number.Value + _number;
        if (x < 0)
        {
            x = maxNumber;
        }
        else if (x > maxNumber)
        {
            x = minNumber;
        }

        number.Value = x;
    }

    private void ChangeSpriteFromNumber(int number)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null && number >= 0 && number < sprites.Count)
        {
            renderer.sprite = sprites[number];
        }
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

    public void OnObjectOnDragged(GameObject obj)
    {

        Debug.Log("tag" + ":" + obj.name);
    }
}
