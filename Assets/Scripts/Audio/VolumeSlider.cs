using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private void Start()
        {
            SoundManager.ChangeMasterVolume(slider.value);
            slider.onValueChanged.AddListener(SoundManager.ChangeMasterVolume);
        }
    }
}
