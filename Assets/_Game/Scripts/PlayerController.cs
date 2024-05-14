using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDragHandler
{
    [SerializeField] private float sensitivity = 50;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Player player;
    [SerializeField] private float boundOffset;
    [SerializeField] private PlayerControllerButton leftBtn, rightBtn;

    private float leftBound, rightBound;
    private Camera mainCam;
    private PointerEventData pointerEventData;

    private void Start()
    {
        DL.Utils.CoroutineUtils.Invoke(this, () =>
        {
            var worldCameraSize = DL.Utils.WorldUtils.GetOrthoCameraWorldSpaceSize(Camera.main, UIController.Instance.RefCanvas);
            this.rightBound = worldCameraSize.x / 2 - (boundOffset * player.transform.localScale.x);
            this.leftBound = -worldCameraSize.x / 2 + (boundOffset * player.transform.localScale.x);
        }, 0);
        mainCam = Camera.main;

        leftBtn.SetCallback(() => {
            player.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        });
        rightBtn.SetCallback(() => {
            player.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        });

    } 

    public void OnDrag(PointerEventData eventData)
    {
        // var currentMouseWorldPos = (Vector2)mainCam.ScreenToWorldPoint(eventData.position);
        // var lastMouseWorldPos = (Vector2)mainCam.ScreenToWorldPoint(eventData.position - eventData.delta);
        // var mouseWorldDelta = currentMouseWorldPos - lastMouseWorldPos;
        // var move = mouseWorldDelta * sensitivity / 100;
        // move.y = 0;
        // var playerPos = player.transform.position;
        // playerPos += (Vector3)move;
        // if (playerPos.x < leftBound) playerPos.x = leftBound;
        // if (playerPos.x > rightBound) playerPos.x = rightBound;
        // player.transform.position = playerPos;
    }
}
