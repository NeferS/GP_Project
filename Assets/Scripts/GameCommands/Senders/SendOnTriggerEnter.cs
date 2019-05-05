using UnityEngine;

/*IMPORTED FROM THE '3DGamekit' FREE ASSETS IN THE UNITY STORE.*/
/*UNUSED*/
public class SendOnTriggerEnter : SendGameCommand
{
    void OnTriggerEnter(Collider other)
    {
        Send();
    }
}