using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SendGameCommand))]
public class CameraSwitchObject : Interactable
{

    void Start()
    {
        base.Init();
    }


    void Update()
    {
        
    }

    public override void RealizeInteraction(GameObject obj)
    {
        GetComponent<SendGameCommand>().Send();
        Interact(true);
        enabled = false;
    }
}
