using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnabler : GameCommandHandler
{
    public override void PerformInteraction(GameCommandType type)
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
