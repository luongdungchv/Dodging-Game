using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Canvas refCanvas;
    [SerializeField] private TMP_Text textScore, textLife;
    public RectTransform RefCanvas => this.refCanvas.GetComponent<RectTransform>();

    private void Awake(){
        Instance = this;
    }
    public void UpdateScore(int score){
        this.textScore.text = score.ToString();
    }
    public void UpdateLife(int lives){
        this.textLife.text = lives.ToString();
    }
    public void Restart(){
        SceneManager.LoadScene("Gameplay");
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }


}
