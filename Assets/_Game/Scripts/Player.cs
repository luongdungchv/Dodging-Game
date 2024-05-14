using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private float invincibleFXInterval;
    [SerializeField] private float invincibleDuration;
    [SerializeField] private float relativeScreenHeight;
    [SerializeField] private float animSpeedIncrement;

    private bool isInvincible;
    private float currentAnimSpd = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.transform.localScale = Vector3.one * relativeScreenHeight / UIController.Instance.RefCanvas.sizeDelta.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            GameManager.Instance.AddScore();
            other.GetComponentInParent<ObstacleCell>().SetAsEmpty();
            Debug.Log("coin");
            SoundManager.instance.PlayOneShot(SFX.Coin_Collect);
        }
        else if (other.tag == "Chest")
        {
            GameManager.Instance.AddScoreChest();
            other.GetComponentInParent<ObstacleCell>().SetAsEmpty();
            SoundManager.instance.PlayOneShot(SFX.Chest_Collect);
        }
        if (this.isInvincible) return;
        Debug.Log(other.tag);
        if (other.tag == "Obstacle")
        {
            Debug.Log(other);
            GameManager.Instance.ReduceLife();
            TriggerInvincible();
            SoundManager.instance.PlayOneShot(SFX.Hurt);
        }

    }

    private void TriggerInvincible()
    {
        this.isInvincible = true;
        var coroutine = DL.Utils.CoroutineUtils.SetInterval(this, (i) =>
        {
            spriteRenderer.enabled = i % 2 == 1;
        }, invincibleFXInterval);
        DL.Utils.CoroutineUtils.Invoke(this, () =>
        {
            StopCoroutine(coroutine);
            this.spriteRenderer.enabled = true;
            this.isInvincible = false;
        }, this.invincibleDuration);
    }
    public void IncreaseAnimSpeed()
    {
        currentAnimSpd += animSpeedIncrement;
        animator.SetFloat("animSpd", currentAnimSpd);
    }
    public void EnableAnimation()
    {
        this.animator.enabled = true;
    }
}
