using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CanvasUIManager : MonoBehaviour
{
    [SerializeField] private Button buttonSubmit;
    [SerializeField] private SpotManager spotManager;

    // Start is called before the first frame update
    void Start()
    {
        spotManager.HasEmptyObjectStream
            .Subscribe(hasEmpty => buttonSubmit.interactable = hasEmpty);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
