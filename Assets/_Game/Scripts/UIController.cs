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
    [SerializeField] private GameObject replayPanel;
    [SerializeField] private GameObject startPanel;
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
        SoundManager.instance.PlayOneShot(SFX.Btn_Click);
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause(){
        SoundManager.instance.PlayOneShot(SFX.Btn_Click);
        Time.timeScale = 0;
    }
    public void Resume(){
        SoundManager.instance.PlayOneShot(SFX.Btn_Click);
        SoundManager.instance.PlayOneShot(SFX.Btn_Click);
        Time.timeScale = 1;
    }
    public void ShowReplay(){
        this.replayPanel.gameObject.SetActive(true);
    }
    public void StartPlaying(){
        SoundManager.instance.PlayOneShot(SFX.Btn_Click);
        startPanel.gameObject.SetActive(false);
        GameManager.Instance.StartCountDown();
    }

}
