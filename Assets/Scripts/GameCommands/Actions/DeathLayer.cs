using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLayer : GameCommandHandler
{
    public GameObject[] checkpoints;
    public GameObject character;
    private int lastCheckpointIndex = 0;

    public override void PerformInteraction(GameCommandType type)
    {
        if(type == GameCommandType.Activate)
        {
            GetComponentInChildren<AudioSource>().Play();
            character.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(WaitForIt());
        }
        else if(type == GameCommandType.Update)
        {
            lastCheckpointIndex++;
        }
    }

    private IEnumerator WaitForIt()
    {
        yield return new WaitForEndOfFrame();
        character.transform.position = checkpoints[lastCheckpointIndex].transform.position;
        character.GetComponent<Rigidbody>().isKinematic = true;
    }
}
