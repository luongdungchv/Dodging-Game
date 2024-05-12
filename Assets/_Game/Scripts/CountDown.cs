using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public void AnimationComplete(){
        GameManager.Instance.StartGame();
        this.gameObject.SetActive(false);
    }
}
