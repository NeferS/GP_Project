using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of Interactable and defines the behaviour of the objects that can switch the camera view.
 *It uses several scripts from the GameCommand system in order to work properly.*/
[RequireComponent(typeof(SendGameCommand))]
public class CameraSwitchObject : Interactable
{
    public bool isOneShot = true;

    void Start()
    {
        base.Init();
    }

    /*When the interaction is triggered, this implementation just uses the GameCommand system. If this interaction can
     *be performed just once, the script is disabled*/
    public override void RealizeInteraction(GameObject obj)
    {
        GetComponent<SendGameCommand>().Send();
        Interact(true);
        if(isOneShot)
            enabled = false;
    }
}
