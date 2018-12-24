using UnityEngine;
using System.Collections;

namespace Yogie
{
    public class GameResources : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip gclip;

        void Start()
        {
            PlayBGM(gclip);
        }

        public void PlayBGM(AudioClip clip)
        {
            if (audioSource.clip == clip)
                return;

            audioSource.Stop();
            audioSource.clip = clip;

            audioSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
