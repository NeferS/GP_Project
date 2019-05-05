using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of GameCommandHandler. Basically, it is used to enable the renderer and the collider of a GameObject
 *in the scene; it is useful to render some objects that fill the scene after a perspective switch.*/
public class SimpleEnabler : GameCommandHandler
{
    public override void PerformInteraction(GameCommandType type)
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
