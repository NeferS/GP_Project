using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script simply sends the right commands to the selected pins of the letters puzzle.*/
public class LettersPuzzleHandler : GameCommandHandler
{
    public GameCommandReceiver[] interactables;
    public float interactionTime = 2.0f;

    public override void PerformInteraction(GameCommandType type)
    {
        StartCoroutine(Interaction(type));
    }

    private IEnumerator Interaction(GameCommandType type)
    {
        /*enables all the interactables objects in 'interactables'*/
        foreach (GameCommandReceiver receiver in interactables)
            receiver.Receive(GameCommandType.Activate);
        yield return new WaitForSeconds(interactionTime);

        /*resets all the interactables objects in 'interactables'*/
        foreach (GameCommandReceiver receiver in interactables)
            receiver.Receive(GameCommandType.Reset);
        yield return new WaitForSeconds(interactionTime);
    }
}
