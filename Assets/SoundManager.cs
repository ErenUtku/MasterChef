using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource, _effectsSource;
    public bool musicOff;
    public bool effectsOff;

    public static SoundManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
        musicOff = !musicOff;
    }

    public void ToggleEffets()
    {
        _effectsSource.mute = !_effectsSource.mute;
        effectsOff = !effectsOff;
    }
}
