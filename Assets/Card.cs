using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// カードクラス
/// 初期化時にスプライトを指定する,null許容
/// </summary>
public class Card : MonoBehaviour, IClickableObject
{
    private bool isDragging = false;
    private ReactiveProperty<int> number = new(0);
    private int maxNumber;
    private int minNumber = 0;
    [SerializeField]
    public List<Sprite> sprites;


    // Start is called before the first frame update
    void Start()
    {

        //
        number.Subscribe(x => ChangeSpriteFromNumber(x))
            .AddTo(this);

        maxNumber = sprites.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnMouseDown()
    {
        Debug.Log("MouseDown detected on " + gameObject.name);
        AddNumber(1);
    }

    public void OnMouseDrag()
    {
        // throw new System.NotImplementedException();
    }

    public void OnMouseUp()
    {
        // throw new System.NotImplementedException();
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
        if (sprites.Count > 0 && sprites.Count <= number)
        {
            renderer.sprite = sprites[number];
        }
    }
}
