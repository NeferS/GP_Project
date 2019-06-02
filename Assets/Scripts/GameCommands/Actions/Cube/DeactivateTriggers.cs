using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTriggers : MonoBehaviour
{
    public Collider[] colliders;
    public float deactivationTime = 1;

    void Update()
    {
        int index = -1;
        for(int i=0; i<colliders.Length;i++)
            if(colliders[i].GetComponent<Animator>().GetBool("Activate"))
            {
                index = i;
                break;
            }

        if(index != -1)
        {
            for (int i = 0; i < colliders.Length; i++)
                if (i != index)
                    colliders[i].enabled = false;
            StartCoroutine(reactivate());
        }
    }

    IEnumerator reactivate()
    {
        yield return new WaitForSeconds(deactivationTime);
        foreach (Collider coll in colliders)
        {
            coll.enabled = true;
        }
    }
}
