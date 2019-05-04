using UnityEngine;

public class SendOnTriggerEnter : SendGameCommand
{
    void OnTriggerEnter(Collider other)
    {
        Send();
    }
}