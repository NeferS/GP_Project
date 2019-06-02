using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentChange : GameCommandHandler
{
    public Transform Cube;
    public SendGameCommand OnStartCommand, OnStopCommand;

    private Collider[] HitColliders;

    public override void PerformInteraction(GameCommandType type)
    {
        if (OnStartCommand != null)
        {
            OnStartCommand.Send();
        }
        if (type == GameCommandType.Start)
        {
            HitColliders = Physics.OverlapBox(transform.position, new Vector3(5, 5, 1), transform.rotation, 1 << 15);
            StartCoroutine(TakeChildren());
        }
        else
        {
            StartCoroutine(ReleaseChildren());
        }
    }

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
