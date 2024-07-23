using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class ClickEventManager : MonoBehaviour
{
    private GameObject currentDraggingObject = null;
    void Start()
    {
        // UpdateAsObservableを使って毎フレームのクリックイベントをチェック
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => DoMouseClick())
            .AddTo(this);

        // マウスエンターの監視
        this.UpdateAsObservable()
            .Where(_ => !Input.GetMouseButtonDown(0))
            .Subscribe(_ => DoMouseOnObject())
            .AddTo(this);

        // マウスリリースの監視
        this.UpdateAsObservable()
            .Where(_ => currentDraggingObject != null)
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => DoMouseOnRelease())
            .AddTo(this);

    }

    void DoMouseClick()
    {
        // マウス位置からのRayを作成
        RaycastHit2D[] hits = GetHits();

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent<IClickableObject>(out var clickable))
            {

                clickable.OnMouseClick();
                // 優先順位の定義などあれば

                // ドラッグできるオブジェクトであればドラッギング状態に格納する
                if (currentDraggingObject == null && clickable.Draggable) { }
                {
                    Debug.Log("tag" + ":" + clickable);
                    currentDraggingObject = hit.collider.gameObject;
                }
                break;
            }
        }


    }

    void DoMouseOnObject()
    {

        // マウス位置からのRayを作成
        RaycastHit2D[] hits = GetHits();

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent<IClickableObject>(out var clickable))
            {

                clickable.OnMouseOnObject();
                // 優先順位の定義などあれば
                break;
            }
        }

    }

    void DoMouseOnRelease()
    {
        // マウス位置からのRayを作成
        RaycastHit2D[] hits = GetHits();
        Debug.Log("tag" + ":" + hits.Length);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log("tag" + ":" + hit.collider.gameObject.name + "");
            if (hit.collider.TryGetComponent<IClickableObject>(out var clickable))
            {

                // clickable.OnMouseOnObject();
                clickable.OnObjectOnDragged(currentDraggingObject);
                // 優先順位の定義などあれば
            }
        }
    }

    RaycastHit2D[] GetHits()
    {

        // マウス位置からのRayを作成
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

        hits = hits.OrderBy(hit => hit.distance).ToArray();
        return hits;
    }
}
