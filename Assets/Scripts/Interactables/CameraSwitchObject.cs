using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of Interactable and defines the behaviour of the objects that can switch the camera view.
 *It uses several scripts from the GameCommand system in order to work properly.*/
[RequireComponent(typeof(SendGameCommand))]
public class CameraSwitchObject : Interactable
{
    public bool isOneShot = true;
    private Animator animator;

    void Start()
    {
        base.Init();
        animator = GetComponent<Animator>();
        if (animator)
            animator.SetBool("OneShot", isOneShot);
    }

    /*When the interaction is triggered, this implementation just uses the GameCommand system. If this interaction can
     *be performed just once, the script is disabled*/
    public override void RealizeInteraction(GameObject obj)
    {
        if (animator)
            animator.SetBool("Activate", true);
        GetComponent<SendGameCommand>().Send();
        Interact(true);
        if(isOneShot)
            enabled = false;
    }
}
