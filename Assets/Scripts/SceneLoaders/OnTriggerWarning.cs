using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OnTriggerWarning : MonoBehaviour
{
    public Controller controller;
    public bool particles = false;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<CharacterInput>())
        {
            if (particles)
            {
                ParticleSystem particles = GetComponent<ParticleSystem>();
                ParticleSystem.MainModule main = particles.main;
                particles.Pause();
                main.loop = false;
                main.duration = 0.5f;
                particles.Play();
            }

            controller.ExitTriggered();
        }
    }
}
