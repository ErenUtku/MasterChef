using UnityEngine;

namespace Audio
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource, effectsSource;
        
        [HideInInspector] public bool musicOff;
        [HideInInspector] public bool effectsOff;

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
            effectsSource.PlayOneShot(clip);
        }

        public static void ChangeMasterVolume(float value)
        {
            AudioListener.volume = value;
        }
        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
            musicOff = !musicOff;
        }

        public void ToggleEffects()
        {
            effectsSource.mute = !effectsSource.mute;
            effectsOff = !effectsOff;
        }
    }
}
