using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ClickEventManager : MonoBehaviour
{
    void Start()
    {
        // UpdateAsObservableを使って毎フレームのクリックイベントをチェック
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => OnMouseDown());
    }

    void OnMouseDown()
    {
        // マウス位置からのRayを作成
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            IClickableObject clickable = hit.collider.GetComponent<IClickableObject>();
            if (clickable != null)
            {
                clickable.OnMouseDown();
            }
        }
    }
}
