using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Base class to send command on different events (see in SendOnTrigger, SendOnBecameVisible etc. for example of subclasses)
/*IMPORTED FROM THE '3DGamekit' FREE ASSETS IN THE UNITY STORE.*/
[SelectionBase]
public class SendGameCommand : MonoBehaviour
{
    //The type of Command to send. This is not link to any UnityEvent and just act as a way to differentiate this command from other in GameCommandHandlers
    public GameCommandType interactionType;
    public GameCommandReceiver interactiveObject;
    public bool oneShot = false;
    public float coolDown = 1;
    public AudioSource onSendAudio;
    public float audioDelay;

    float lastSendTime;
    bool isTriggered = false;

    public float Temperature
    {
        get
        {
            return 1f - Mathf.Clamp01(Time.time - lastSendTime);
        }
    }

    [ContextMenu("Send Interaction")]
    public void Send()
    {
        if (oneShot && isTriggered) return;
        if (Time.time - lastSendTime < coolDown) return;
        isTriggered = true;
        lastSendTime = Time.time;
        interactiveObject.Receive(interactionType);
        if (onSendAudio) onSendAudio.PlayDelayed(audioDelay);
    }
    
}


