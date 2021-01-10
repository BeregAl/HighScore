using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDialog : DialogManager
{
    public SettingsToggle sound;
    public SettingsToggle music;
    
    private void Start()
    {
        music.SetStateImmediate(GameManager.instance.audioManager.MusicOn);
        sound.SetStateImmediate(GameManager.instance.audioManager.SoundOn);
    }
    
    public void OnMusicClick()
    {
        var state = GameManager.instance.audioManager.ToggleMusic();
        GameManager.instance.audioManager.PlayClick();
        music.SetState(state);
    }

    public void OnSoundClick()
    {
        var state = GameManager.instance.audioManager.ToggleSound();
        GameManager.instance.audioManager.PlayClick();
        sound.SetState(state);
    }
}
