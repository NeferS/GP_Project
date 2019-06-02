using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script casts a Ray from the character position and allows to the player to interact with the intractable objects.*/
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterInteractions : MonoBehaviour
{
    private const float distanceFromInteractable = 1.0f;

    void Update()
    {
        Ray ray = new Ray(transform.position + Vector3.up, GetComponentInChildren<Animator>().transform.forward);
        //Debug.DrawRay(transform.position, GetComponentInChildren<Animator>().transform.forward, Color.green);

        RaycastHit hit;
        GameObject hitObject = null;
        if (Physics.Raycast(ray, out hit))
        {
            hitObject = hit.transform.gameObject;
            Interactable interactable = hitObject.GetComponent<Interactable>();
            if (interactable != null && interactable.enabled)
            {
                if (hit.distance <= distanceFromInteractable && !interactable.isInteracting())
                {
                    hitObject.GetComponent<Interactable>().Activate(true);
                }
                else if (hit.distance > distanceFromInteractable) { hitObject.GetComponent<Interactable>().Activate(false); }
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && hitObject != null && hit.distance <= distanceFromInteractable)
        {

            hitObject.GetComponent<Interactable>().RealizeInteraction(gameObject);
        }
    }
}
