using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Canvas refCanvas;
    [SerializeField] private TMP_Text textScore, textLife;
    [SerializeField] private GameObject replayPanel;
    [SerializeField] private GameObject startPanel, clearPanel;
    [SerializeField] private Sprite soundOn, soundOff;
    [SerializeField] private Button toggleSoundBtn;
    public RectTransform RefCanvas => this.refCanvas.GetComponent<RectTransform>();

    private void Awake(){
        Instance = this;
        toggleSoundBtn.onClick.AddListener(ToggleSound);
        
    }
    private void Start() {
        toggleSoundBtn.GetComponent<Image>().sprite = SoundManager.instance.IsMute ? soundOff : soundOn;
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
    // public void Pause(){
    //     SoundManager.instance.PlayOneShot(SFX.Btn_Click);
    //     if(startPanel.activeSelf) return;
    //     this.pausePanel.gameObject.SetActive(true);
    //     Time.timeScale = 0;
    // }
    // public void Resume(){
    //     SoundManager.instance.PlayOneShot(SFX.Btn_Click);
    //     SoundManager.instance.PlayOneShot(SFX.Btn_Click);
    //     this.pausePanel.gameObject.SetActive(false);
    //     Time.timeScale = 1;
    // }
    public void ShowReplay(){
        this.replayPanel.gameObject.SetActive(true);
    }
    public void StartPlaying(){
        SoundManager.instance.PlayOneShot(SFX.Btn_Click);
        startPanel.gameObject.SetActive(false);
        GameManager.Instance.StartCountDown();
    }
    public void ToggleClearDataPanel(){
        this.clearPanel.gameObject.SetActive(!clearPanel.gameObject.activeSelf);
        SoundManager.instance.PlayOneShot(SFX.Btn_Click);
    }
    public void ClearData(){
        SoundManager.instance.PlayOneShot(SFX.Btn_Click);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("TOS", 1);
        SceneManager.LoadScene("Gameplay");
    }
    public void ToggleSound(){
        var state = SoundManager.instance.ToggleMute();
        toggleSoundBtn.GetComponent<Image>().sprite = state ? soundOff : soundOn;   
    }

}
