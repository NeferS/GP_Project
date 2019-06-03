using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This simple script manages the fire emission in the 'FlameThrower' trap.*/
[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Collider))]
public class FireController : MonoBehaviour
{
    public float deltaTime = 5.0f;
    private ParticleSystem particle;
    private bool start = true;
    private AudioSource _source;
    [SerializeField] AudioClip clip;
    private new Collider collider;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        _source = GetComponentInChildren<AudioSource>();
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }


    void Update()
    {
        if(start)
            StartCoroutine(Play());
    }

    /*The flames are emitted for a given delta time and, then, they disappear for the same amount of time. This coroutine is
     *called in the 'Update' method every time it ends it execution.*/
    private IEnumerator Play()
    {
        start = false;
        _source.PlayOneShot(clip);
        particle.Play();
        collider.enabled = true;
        yield return new WaitForSeconds(deltaTime);
        particle.Stop();
        collider.enabled = false;
        yield return new WaitForSeconds(deltaTime);
        start = true;
    }
}
