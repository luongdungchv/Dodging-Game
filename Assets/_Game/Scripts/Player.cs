using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float invincibleFXInterval;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float invincibleDuration;
    [SerializeField] private float relativeScreenHeight;

    private bool isInvincible;

    private void Start(){
        this.transform.localScale = Vector3.one * relativeScreenHeight / UIController.Instance.RefCanvas.sizeDelta.y;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(this.isInvincible) return;
        if(other.tag == "Obstacle"){
            Debug.Log(other);
            GameManager.Instance.ReduceLife();
            TriggerInvincible();
        }
    }

    private void TriggerInvincible(){
        this.isInvincible = true;
        var coroutine = DL.Utils.CoroutineUtils.SetInterval(this, (i) => {
            spriteRenderer.enabled = i % 2 == 1;
        }, invincibleFXInterval);
        DL.Utils.CoroutineUtils.Invoke(this, () => {
            StopCoroutine(coroutine);
            this.spriteRenderer.enabled = true;
            this.isInvincible = false;
        }, this.invincibleDuration);
    }
}
