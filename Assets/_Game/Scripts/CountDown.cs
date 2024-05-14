using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public void AnimationComplete()
    {
        GameManager.Instance.StartGame();
        this.gameObject.SetActive(false);
    }
    public void PlayCountDownSFX()
    {
        SoundManager.instance.PlayOneShot(SFX.Countdown);
    }
    public void PlayCountDownSFXFinal()
    {
        SoundManager.instance.PlayOneShot(SFX.Countdown_Done);
    }
}
