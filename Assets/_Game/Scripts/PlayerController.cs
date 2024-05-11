using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDragHandler
{
    [SerializeField] private float sensitivity = 50;
    [SerializeField] private Player player;
    public void OnDrag(PointerEventData eventData)
    {
        var move = eventData.delta * sensitivity / 100 * Time.deltaTime;
        move.y = 0;
        player.transform.position += (Vector3)move;
    }
}
