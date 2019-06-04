using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendOnTriggerStay : SendGameCommand
{
    void OnTriggerStay(Collider other)
    {
        Send();
    }
}
