using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*MUST BE MERGED WITH THE NEW MOVEMENT SCRIPT*/
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterInteractions : MonoBehaviour
{
    private const float distanceFromInteractable = 1.0f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward*5f, Color.green);

        RaycastHit hit;
        GameObject hitObject = null;
        if (Physics.SphereCast(ray, 1.5f, out hit))
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
            Debug.Log("si");
            hitObject.GetComponent<Interactable>().RealizeInteraction(gameObject);
        }
    }
}
