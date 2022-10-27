using UnityEngine;

namespace Audio
{
    public class PlaySoundOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;

        void Start()
        {
            SoundManager.instance.PlaySound(clip);
        }
    }
}
