using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [Sirenix.OdinInspector.Button]
    private void Testt(){
        Debug.Log((mask & (1 << this.gameObject.layer)) != 0);
        Debug.Log(mask.value);
        Debug.Log(gameObject.layer);
    }
}
