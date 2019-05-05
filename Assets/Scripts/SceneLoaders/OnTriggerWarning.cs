using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Simple script that invokes the 'ExitTriggered' of the Controller object when the collider of this GameObject is triggered by
 *another collider.*/
[RequireComponent(typeof(Collider))]
public class OnTriggerWarning : MonoBehaviour
{
    public Controller controller;

    /*Invokes the 'ExitTriggered' method on the Controller*/
    void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<CharacterInput>())
        {
            controller.ExitTriggered();
        }
    }
}
