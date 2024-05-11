using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Canvas refCanvas;
    public RectTransform RefCanvas => this.refCanvas.GetComponent<RectTransform>();

    private void Awake(){
        Instance = this;
    }
}
