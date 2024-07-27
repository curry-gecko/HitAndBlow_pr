using UniRx;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // サブジェクトの定義
    public Subject<(IClickableObject, IClickableObject)> OnObjectsReleased = new();

    // サブジェクトのインスタンスをシングルトンとして取得できるようにする
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EventManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("EventManager");
                    _instance = obj.AddComponent<EventManager>();
                }
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        // オブジェクトが破棄された時にサブジェクトを完了させる
        OnObjectsReleased.OnCompleted();
    }
}
