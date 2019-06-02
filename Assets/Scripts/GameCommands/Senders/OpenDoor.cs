using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This simple class controls if each face of the LettersPuzzle object has a y rotation equal to 0 (or 360) and, if it
 *happens, sends the game command.*/
public class OpenDoor : SendOnEvent
{
    [SerializeField] private Transform[] pins;
    private float eps = 0.01f;

    public override bool IsEventTriggered()
    {
        bool val = true;
        foreach (Transform pin in pins)
            if (pin.localEulerAngles.y > eps)
                val = false;
        return val;
    }

    public override void OnEventTrigger()
    {
        Send();
        enabled = false;
    }
}
