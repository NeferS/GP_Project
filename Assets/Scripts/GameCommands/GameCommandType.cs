using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This enumeration declares the type of GameCommand that can be sent, received and handled by senders, receivers and handlers.
 *Used to have a better specification on the type of interaction between these objects.*/
public enum GameCommandType
{
    None,
    Activate,
    Deactivate,
    Open,
    Close,
    Start,
    Reset
}

