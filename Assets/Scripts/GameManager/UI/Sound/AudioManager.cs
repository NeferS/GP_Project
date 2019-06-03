using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    [SerializeField] private string levelBGMusic;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;

    public void Startup()
    {

        musicSource.ignoreListenerPause = true;
        musicSource.ignoreListenerVolume = true;
        soundVolume = 1f;
        musicVolume = 0.1f;
        PlayLevelMusic();
        status = ManagerStatus.Started;
    }

    public float musicVolume
    {
        get
        {
            return musicSource.volume;
        }
        set
        {
            if (musicSource != null)
            {
                musicSource.volume = value;
            }
        }
    }

    public bool musicMute
    {
        get
        {
            if (musicSource != null)
            {
                return musicSource.mute;
            }
            return false;
        }
        set
        {
            if (musicSource != null)
            {
                musicSource.mute = value;
            }
        }
    }

    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    public void PlayLevelMusic()
    {
        PlayMusic((AudioClip)Resources.Load("Music/" + levelBGMusic));
    }

    private void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
