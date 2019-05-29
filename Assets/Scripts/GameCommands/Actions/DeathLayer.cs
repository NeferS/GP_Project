using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of 'GameCommandHandler'.*/
public class DeathLayer : GameCommandHandler
{
    public GameObject[] checkpoints;
    public GameObject character;
    private int lastCheckpointIndex = 0;

    /*If the received command is 'Activate', then it places the character in the last checkpoint position. If
     *the received command is 'Update', then it increases the index for the checkpoints array so when an 'Activate' command
     *occurs the player will be placed in the next checkpoint.*/
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

    /*If the playes is not placed in the checkpoint position after the frame ends, then the changes on its Rigidbody
     *wouldn't have effect.*/
    private IEnumerator WaitForIt()
    {
        yield return new WaitForEndOfFrame();
        character.transform.position = checkpoints[lastCheckpointIndex].transform.position;
        character.GetComponent<Rigidbody>().isKinematic = true;
    }
}
