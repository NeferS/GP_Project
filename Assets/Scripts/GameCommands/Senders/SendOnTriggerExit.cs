using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*IMPORTED FROM THE '3DGamekit' FREE ASSETS IN THE UNITY STORE.*/
/*UNUSED*/
public class SendOnTriggerExit : SendGameCommand
{

    void OnTriggerExit(Collider other)
    {
        Send();
    }
}
