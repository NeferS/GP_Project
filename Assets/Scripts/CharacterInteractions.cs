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

        RaycastHit hit;
        GameObject hitObject = null;
        if (Physics.Raycast(ray, out hit))
        {
            hitObject = hit.transform.gameObject;
            Interactable interactable = hitObject.GetComponent<Interactable>();
            if (interactable && interactable.enabled)
            {
                if (hit.distance <= distanceFromInteractable && !interactable.isInteracting())
                {
                    hitObject.GetComponent<Interactable>().Activate(true);
                }
                else if (hit.distance > distanceFromInteractable) { hitObject.GetComponent<Interactable>().Activate(false); }
            }
            else
                hitObject = null;
        }
        if (Input.GetKeyDown(KeyCode.E) && hitObject && hit.distance <= distanceFromInteractable)
        {
            hitObject.GetComponent<Interactable>().RealizeInteraction(gameObject);
        }
    }
}
