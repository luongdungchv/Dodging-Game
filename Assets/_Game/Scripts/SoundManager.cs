using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SoundManager : SerializedMonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private Dictionary<SFX, AudioClip> audioList;
    private bool mute;
    public bool IsMute => mute;

    private void Awake() {
        if(instance != null){
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        mute = PlayerPrefs.GetInt("mute", 0) != 0;
    }

    public void PlayOneShot(SFX sfx){
        if(mute) return;
        soundPlayer.PlayOneShot(audioList[sfx]);
    }
    [Sirenix.OdinInspector.Button]
    private void TestSound(SFX sfx){
        PlayOneShot(sfx);
    }
    public bool ToggleMute(){
        this.mute = !this.mute;
        PlayerPrefs.SetInt("mute", this.mute ? 1 : 0);
        return this.mute;
    }
}
public enum SFX{
    Btn_Click, Coin_Collect, Chest_Collect, Hurt, Countdown, Countdown_Done
}
