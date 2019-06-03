using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class extend the GameCommandHandler and is used in the cube puzzle to get the right gameobject to rotate with the rotor.*/
public class ParentChange : GameCommandHandler
{
    public Transform Cube;
    public SendGameCommand OnStartCommand, OnStopCommand;

    private Collider[] HitColliders;

    /*when invoked, this method is executed*/
    public override void PerformInteraction(GameCommandType type)
    {
        if (OnStartCommand != null)
        {
            OnStartCommand.Send();
        }
        if (type == GameCommandType.Start)
        {
            /*The OverlapBox is used to detect the nearby object*/
            HitColliders = Physics.OverlapBox(transform.position, new Vector3(5, 5, 1), transform.rotation, 1 << 15);
            StartCoroutine(TakeChildren());
        }
        else
        {
            StartCoroutine(ReleaseChildren());
        }
    }

    /*This coroutine sets this object as parent for all the colliders in HitColliders*/ 
    private IEnumerator TakeChildren()
    {
        foreach (Collider coll in HitColliders)
        {
            coll.transform.SetParent(transform);
        }
        yield return null;
        if (OnStartCommand != null)
        {
            OnStartCommand.Send();
        }
    }

    /*This coroutine sets the Cube as parent for all the colliders in HitColliders*/
    private IEnumerator ReleaseChildren()
    {
        while (transform.childCount > 0)
            transform.GetChild(0).SetParent(Cube);
        yield return null;
        if (OnStopCommand != null)
        {
            OnStopCommand.Send();
        }
    }

}
