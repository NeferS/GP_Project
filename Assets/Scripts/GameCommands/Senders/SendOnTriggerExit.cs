using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SendOnTriggerExit : SendGameCommand
{

    void OnTriggerExit(Collider other)
    {
        Send();
    }
}
