using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> click;
    public List<AudioClip> lose;
    public List<AudioClip> success;
    public List<AudioClip> powerUp;
    
    [Space] public AudioSource musicAudioSource;
    public AudioSource soundsAudioSource;
    
    public bool MusicOn => musicOn;
    [Space] [SerializeField] private bool musicOn = true;
        
    public bool SoundOn => soundOn;
    [SerializeField] private bool soundOn = true;
    
    protected internal void InitAudioConfig()
    {
        musicOn = PlayerPrefs.GetInt("MusicKey", 1) == 1;
        soundOn = PlayerPrefs.GetInt("SoundKey", 1) == 1;

        if (musicOn)
        {
            musicAudioSource.Play();
        }
    }
    
    public bool ToggleMusic()
    {
        musicOn = !musicOn;
        PlayerPrefs.SetInt("MusicKey", musicOn ? 1 : 0);

        if (musicOn)
        {
            musicAudioSource.Play();
        }
        else
        {
            musicAudioSource.Pause();
        }
        
        return musicOn;
    }

    public bool ToggleSound()
    {
        soundOn = !soundOn;
        PlayerPrefs.SetInt("SoundKey", soundOn ? 1 : 0);
        return soundOn;
    }
    
    public void PlayClick()
    {
        if (!soundOn) return;

        soundsAudioSource.pitch = Random.Range(0.9f, 1.1f);
        soundsAudioSource.PlayOneShot(click[Random.Range(0, click.Count)]);
    }
    
    public void PlayLose()
    {
        if (!soundOn) return;

        soundsAudioSource.pitch = 1f;
        soundsAudioSource.PlayOneShot(lose[Random.Range(0, lose.Count)]);
    }
    
    public void PlaySuccess()
    {
        if (!soundOn) return;

        soundsAudioSource.pitch = 1f;
        soundsAudioSource.PlayOneShot(success[Random.Range(0, success.Count)]);
    }
    
    public void PlayPowerUp()
    {
        if (!soundOn) return;

        soundsAudioSource.pitch = 1f;
        soundsAudioSource.PlayOneShot(powerUp[Random.Range(0, powerUp.Count)]);
    }
}
