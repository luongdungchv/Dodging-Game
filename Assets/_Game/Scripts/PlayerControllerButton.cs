using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerControllerButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    private UnityAction Callback;
    private bool holding;

    public void OnPointerDown(PointerEventData eventData)
    {
        holding = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        holding = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holding = false;
    }

    private void Update(){
        if(holding) this.Callback?.Invoke();
    }

    public void SetCallback(UnityAction callback){
        this.Callback = callback;
    }
}
