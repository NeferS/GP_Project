using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class manages the AudioSource related to a ParticleSystem. It manages all the AudioClip that has to be played during the
 *simulation of the particles and manages specific events that can occur (for example OnTriggerEnter, ecc).*/
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
public class ParticleManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip loopSound, /*the sound effect played during the life of the particles*/
                      onTriggerSound; /*the sound effect that must be played if the particles are triggered*/

    private ParticleSystem _particles;
    private AudioSource _audioSource;

    /*this variable is used to disable the GameObject of the particles after a fixed delay; it should be great enough to
     *let the particles finish the last simulation*/
    private const float timeToDisable = 1.8f;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        _particles = GetComponent<ParticleSystem>();
        _audioSource.spatialBlend = 1.0f;
        if(loopSound)
        {
            _audioSource.clip = loopSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    /*If the player enters in the trigger of this GameObject, the ParticleSystem must be disabled (and disappear from the scene
     *as soon as possible)*/
    void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<CharacterInput>())
        {
            _audioSource.Stop();
            if (onTriggerSound)
            {
                _audioSource.PlayOneShot(onTriggerSound);
            }
            ParticleSystem.MainModule main = _particles.main;
            _particles.Pause();
            main.loop = false;
            main.duration = 1.0f;
            main.simulationSpeed = 2.5f;
            _particles.Play();
            StartCoroutine(Disable());
        }
    }

    protected IEnumerator Disable()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
}
