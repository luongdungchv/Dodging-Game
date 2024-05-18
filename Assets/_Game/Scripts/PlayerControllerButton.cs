using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerControllerButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IDropHandler, IDragHandler
{
    [SerializeField] private string debugString;
    private UnityAction Callback;
    private bool holding;
    private PointerEventData currentEventData;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.currentEventData = eventData;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    private void Update(){
        if(currentEventData == null) return;
        if(currentEventData.pointerDrag != null && currentEventData.pointerDrag == this.gameObject) this.Callback?.Invoke();
    }

    public void SetCallback(UnityAction callback){
        this.Callback = callback;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
