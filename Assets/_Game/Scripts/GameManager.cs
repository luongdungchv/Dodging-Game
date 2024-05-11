using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lifeCount;
    public static GameManager Instance;
    [HideInInspector] public UnityEvent OnGameOver;
    private int currentLives;

    private void Awake()
    {
        Instance = this;
        this.OnGameOver = new UnityEvent();
    }
    private void Start(){
        currentLives = lifeCount;
    }
    public void ReduceLife(){
        currentLives--;
        if(currentLives <= 0){
            this.GameOver();
        }
    }
    public void GameOver()
    {
        this.OnGameOver?.Invoke();
    }
    
    
}
