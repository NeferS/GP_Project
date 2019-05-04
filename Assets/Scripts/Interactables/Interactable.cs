using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Base class for the gerarchy of interactable objects; an interactable object can be recognized using this base class.
 *The class provides some default methods and the main abstract method to perform interactions: RealizeInteraction(GameObject obj).*/
public abstract class Interactable : MonoBehaviour
{
    /*The 'E' object*/
    [SerializeField] private GameObject _interactIcon;
    /*If 'true' the object has interacted or is still interacting*/
    private bool interacted = false;

    void Start()
    {
        Init();
    }

    /*Deactivates the interaction icon*/
    protected void Init() { _interactIcon.SetActive(false); }
    /*Activates the interaction icon*/
    public void Activate(bool activate) { _interactIcon.SetActive(activate); }
    /*Sets the 'interacted' value and activates (or deactivates) the interaction action using the
     *'!interact' value*/
    protected void Interact(bool interact)
    {
        interacted = interact;
        _interactIcon.SetActive(!interact);
    }
    /*Returns 'true' if the object is interacting or has interacted (which means the variable
     *'interacted' is true, 'false' otherwise*/
    public bool isInteracting() { return interacted; }
    /*Abstract method that the subclasses must implement to perform the interaction*/
    public abstract void RealizeInteraction(GameObject obj);

}
