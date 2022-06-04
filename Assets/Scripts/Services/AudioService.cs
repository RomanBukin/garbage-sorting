using UnityEngine;

namespace Services
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource soundAudioSource;
        
        [Header("Audio clips")]
        [SerializeField] private AudioClip menuMusic;
        [SerializeField] private AudioClip[] gameplayMusic;
        [SerializeField] private AudioClip[] sounds;

        public void PlayMenuMusic()
        {
            PlayMusicAudioClip(menuMusic);
        }
    
        public void PlayGameplayMusic(int index)
        {
            if (index < gameplayMusic.Length)
            {
                PlayMusicAudioClip(gameplayMusic[index]);
            }
        }

        private void PlayMusicAudioClip(AudioClip clip)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public void StopMusic()
        {
            musicAudioSource.Stop();
        }

        public void PlaySound(int index)
        {
            if (index < sounds.Length)
            {
                soundAudioSource.PlayOneShot(sounds[index]);
            }
        }
    }
}
