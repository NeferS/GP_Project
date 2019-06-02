using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of Interactable and defines the behaviour of the objects that can switch the pins in the letters puzzle.
 *It uses several scripts from the GameCommand system in order to work properly.*/
[RequireComponent(typeof(SendGameCommand))]
public class LettersPuzzleSwitch : Interactable
{
    [SerializeField] private LettersPuzzleSwitch[] group;
    public bool isOneShot = false;
    private const float interactTime = 4.0f;

    /*When the interaction is triggered, this implementation just uses the GameCommand system. If this interaction can
     *be performed just once, the script is disabled*/
    public override void RealizeInteraction(GameObject obj)
    {
        /*If the other switches are interacting, this switch can't interact*/
        bool canInteract = true;
        foreach(LettersPuzzleSwitch interactable in group)
            if(interactable.isInteracting())
            {
                canInteract = false;
                break;
            }

        if(canInteract)
        {
            GetComponent<SendGameCommand>().Send();
            Interact(true);
            if (isOneShot)
                enabled = false;
            StartCoroutine(WaitForNext());
        }
    }

    private IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(interactTime);
        Interact(false);
        Activate(false);
    }
}
