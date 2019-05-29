using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*IMPORTED FROM THE '3DGamekit' FREE ASSETS IN THE UNITY STORE.*/
public class SendOnTriggerExit : SendGameCommand
{

    void OnTriggerExit(Collider other)
    {
        Send();
    }
}
