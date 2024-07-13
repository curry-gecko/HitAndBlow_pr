using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class ClickEventManager : MonoBehaviour
{
    void Start()
    {
        // UpdateAsObservableを使って毎フレームのクリックイベントをチェック
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => OnMouseDown())
            .AddTo(this);
    }

    void OnMouseDown()
    {
        // マウス位置からのRayを作成
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

        if (hits.Length == 0) return;
        hits = hits.OrderBy(hit => hit.distance).ToArray();

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent<IClickableObject>(out var clickable))
            {

                clickable.OnMouseClick();
                // 優先順位の定義などあれば
                break;
            }
        }
    }
}
