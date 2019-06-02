using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script con teleport the character to the target position.*/
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Teleporter : MonoBehaviour
{
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private AudioClip onTeleportAudio;
    private bool isOneShot = false;
    private bool triggered = false;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    /*If the character has a Rigidbody, the variable 'isKinematic' has to be set to 'false' in order to make this script work.*/
    void OnTriggerEnter(Collider collider)
    {
        if (isOneShot && triggered) return;
        if(collider.GetComponent<CharacterInput>())
        {
            triggered = true;
            if (onTeleportAudio)
                GetComponent<AudioSource>().PlayOneShot(onTeleportAudio);

            if(collider.GetComponent<Rigidbody>())
            {
                collider.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(WaitForIt(collider));
            }
            else
                collider.transform.position = targetPos;

        }
    }

    /*If the object with a Rigidbody was not placed in the checkpoint position after a delta time, the changes on its Rigidbody
     *wouldn't have had effect.*/
    private IEnumerator WaitForIt(Collider obj)
    {
        yield return new WaitForEndOfFrame();
        obj.transform.position = targetPos;
        obj.GetComponent<Rigidbody>().isKinematic = true;
    }
}
