using UnityEngine;
/// <summary>
/// ピンに追従するテキストオブジェクト(デバッグ用)
/// </summary>using UnityEngine;
using TMPro;
using UniRx;

public class PinWithText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Pin pin = null;

    private void Start()
    {
    }

    private void Update()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main camera not found");
            return;
        }

        if (text != null && pin != null)
        {
            // テキストをピンオブジェクトの位置に追従させる
            text.transform.position = Camera.main.WorldToScreenPoint(pin.transform.position);
        }
    }

    public void UpdateText(string typeName)
    {
        if (text != null)
        {
            text.text = typeName;
        }
    }

    private void AttachPin(Pin _pin)
    {
        pin = _pin;
        pin.TypeName.Subscribe(name => UpdateText(name)).AddTo(this);
    }


    // 
    public static PinWithText Init(Pin pin)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null) return null;

        GameObject prefab = Resources.Load<GameObject>("PinWithText");
        PinWithText pwt = Instantiate(prefab, canvas.transform).GetComponent<PinWithText>();
        pwt.AttachPin(pin);

        return pwt;
    }
}
