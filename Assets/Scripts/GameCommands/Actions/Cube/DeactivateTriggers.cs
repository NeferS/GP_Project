using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script manages the concurrent interaction between two or more pressure pads.*/
public class DeactivateTriggers : MonoBehaviour
{
    public Collider[] colliders;
    public float deactivationTime = 1;

    void Update()
    {
        /*Searches for the index of the pressure pad which is interacting, if it exists*/
        int index = -1;
        for(int i=0; i<colliders.Length;i++)
            if(colliders[i].GetComponent<Animator>().GetBool("Activate"))
            {
                index = i;
                break;
            }

        /*If the inndex is valid, then all the colliders of the pressure pad (except for the one at index position) are disabled*/
        if(index != -1)
        {
            for (int i = 0; i < colliders.Length; i++)
                if (i != index)
                    colliders[i].enabled = false;
            StartCoroutine(Reset());
        }
    }

    /*After a delta time all the collider will be enabled again*/
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(deactivationTime);
        foreach (Collider coll in colliders)
        {
            coll.enabled = true;
        }
    }
}
