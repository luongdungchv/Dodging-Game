using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCell : MonoBehaviour
{
    [SerializeField] private GameObject wall, coin, chest;
    [SerializeField] private float chestProb;

    public void SetAsWall(){
        this.coin.SetActive(false);
        this.wall.SetActive(true);
        this.chest.SetActive(false);
    }
    public void DetermineScorer(){
        var r = Random.Range(0, 100);
        if(r <= chestProb) this.SetAsChest();
        else this.SetAsCoin();
    }
    public void SetAsCoin(){
        this.coin.SetActive(true);
        this.wall.SetActive(false);
        this.chest.SetActive(false);
    }
    public void SetAsChest(){
        this.coin.SetActive(false);
        this.wall.SetActive(false);
        this.chest.SetActive(true);
    }
    public void SetAsEmpty(){
        this.coin.SetActive(false);
        this.wall.SetActive(false);
        this.chest.SetActive(false);
    }
    
}
