using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of 'SendGameCommand'. In the Update() method, it just controls if the event has been triggered and, 
 *if it happens, invokes the OnEventTrigger() method.*/
public abstract class SendOnEvent : SendGameCommand
{
    public bool forceEvent = false;

    void Update()
    {
        if(IsEventTriggered() || forceEvent)
        {
            OnEventTrigger();
        }
    }

    public abstract bool IsEventTriggered();

    public abstract void OnEventTrigger();
}
