using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCell : MonoBehaviour
{
    private bool isScore;
    [SerializeField] private GameObject wall, scorer;

    public void SetAsWall(){
        this.isScore = false;
        this.scorer.SetActive(false);
        this.wall.SetActive(true);
    }
    public void SetAsScorer(){
        this.isScore = true;
        this.scorer.SetActive(true);
        this.wall.SetActive(false);
    }
    
}
