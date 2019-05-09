using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This simple script manages the AudioSource that plays the main clip in the main menu scene.*/
[RequireComponent(typeof(AudioSource))]
public class MainMenuSoundManager : MonoBehaviour
{
    /*Main clip that will be looped*/
    [SerializeField] private AudioClip loopClip;
    /*Intro clip for the main clip*/
    private AudioClip startClip;
    private AudioSource _audioSource;

    private bool isFading = false;
    private float fadePerStep = 0.007f;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        startClip = _audioSource.clip;
    }


    void Update()
    {
        /*If the intro clip has ended and the loop clip is not null (and the AudioSource is not fading out),
         *than the loop clip begins to play*/
        if (!_audioSource.isPlaying && loopClip && !isFading)
        {
            _audioSource.loop = true;
            _audioSource.clip = loopClip;
            _audioSource.Play();
        } else if (isFading)
        {
            /*Simple fade-out system, used (for example) when the current scene is changing*/
            float newVolume = _audioSource.volume - fadePerStep;
            if (newVolume <= 0.0f)
            {
                newVolume = 0.0f;
                _audioSource.Stop();
                enabled = false;
            }
            _audioSource.volume = newVolume;
        }
    }

    /*Basically, marks as 'true' the boolean that enables the fade-out system*/
    public void Fade() { isFading = true; }
}
