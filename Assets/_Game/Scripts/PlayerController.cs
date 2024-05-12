using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDragHandler
{
    [SerializeField] private float sensitivity = 50;
    [SerializeField] private Player player;
    [SerializeField] private float boundOffset;

    private float leftBound, rightBound;


    private void Start()
    {
        DL.Utils.CoroutineUtils.Invoke(this, () =>
        {
            var worldCameraSize = DL.Utils.WorldUtils.GetOrthoCameraWorldSpaceSize(Camera.main, UIController.Instance.RefCanvas);
            this.rightBound = worldCameraSize.x / 2 - (boundOffset * player.transform.localScale.x);
            this.leftBound = -worldCameraSize.x / 2 + (boundOffset * player.transform.localScale.x);
        }, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var move = eventData.delta * sensitivity / 100 * Time.deltaTime / UIController.Instance.RefCanvas.GetComponent<Canvas>().scaleFactor;
        move.y = 0;
        var playerPos = player.transform.position;
        playerPos += (Vector3)move;
        if (playerPos.x < leftBound) playerPos.x = leftBound;
        if (playerPos.x > rightBound) playerPos.x = rightBound;
        player.transform.position = playerPos;
    }
}
