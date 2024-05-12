using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lifeCount, score;
    [SerializeField] private int scorePerCoin, scorePerChest;
    public static GameManager Instance;
    [HideInInspector] public UnityEvent OnGameOver;
    private int currentLives;

    private UIController uiController => UIController.Instance;

    private void Awake()
    {
        Instance = this;
        this.OnGameOver = new UnityEvent();
    }
    private void Start(){
        currentLives = lifeCount;
        uiController.UpdateLife(this.lifeCount);
        this.uiController.UpdateScore(this.score);
    }
    public void ReduceLife(){
        currentLives--;
        if(currentLives <= 0){
            this.GameOver();
        }
        this.uiController.UpdateLife(this.lifeCount);
    }
    public void AddScore(){
        this.score += scorePerCoin;
        this.uiController.UpdateScore(this.score);
    }
    public void AddScoreChest(){
        this.score += scorePerChest;
        this.uiController.UpdateScore(this.score);
    }
    public void GameOver()
    {
        this.OnGameOver?.Invoke();
    }
    
    
}
