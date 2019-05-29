using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of 'GameCommandHandler'.*/
[RequireComponent(typeof(Collider))]
public class DoorExitEnabler : GameCommandHandler
{
    /*If the received command is 'Activate', then the exit door system is activated. Otherwise, if the received command
     *is 'Deactivate', the exit door system is deactivated.*/
    public override void PerformInteraction(GameCommandType type)
    {
        if(type == GameCommandType.Activate)
        {
            GetComponent<Collider>().enabled = true;
            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach(ParticleSystem particle in particles)
                particle.Play();
        } else if(type == GameCommandType.Deactivate)
        {
            GetComponent<Collider>().enabled = false;
            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in particles)
                particle.Stop();
        }
    }
}
