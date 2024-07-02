using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.PackageManager;
using UnityEngine;

/// <summary>
/// ピンの種類､初期化方法の管理
/// </summary>
public class PinManager : MonoBehaviour
{
    // 操作可能なピンのリスト
    List<Pin> activePinList = new List<Pin>();
    [SerializeField] GameObject PinPrefab;
    [SerializeField] float PinWidth = 2.5f;
    [SerializeField] float PinHeight = 3.0f;
    [SerializeField] float PinDepth = 10.0f;
    void Awake()
    {

        if (PinPrefab == null)
        {
            Debug.LogError("Error: PinPrefab is not set in the inspector", this);
        }
    }

    void Start()
    {

    }


    void Update()
    {

    }

    //
    public void AddPin(List<SuitType> suitTypes)
    {
        if (PinPrefab == null) return;

        for (int i = 0; i < suitTypes.Count; i++)
        {
            SuitType item = suitTypes[i];
            GameObject prefab = Instantiate(PinPrefab);
            Pin pin = prefab.GetComponent<Pin>();

            //
            pin.SetTypeName(item.GetDescription());
            pin.transform.position = new Vector3(
                PinWidth * i + transform.position.x,
                PinHeight + transform.position.y,
                PinDepth
            );
            //
            activePinList.Add(pin);
#if UNITY_EDITOR
            // debug
            PinWithText pwt = PinWithText.Init(pin);
#endif
        }
    }
}
