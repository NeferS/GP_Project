using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of Interactable and defines the behaviour of the objects that can be pushed and pulled.
 *It requires that the object which is interacting with has a Rigidbody component.*/
[RequireComponent(typeof(Rigidbody))]
public class PullableObject : Interactable
{
    /*The Rigidbody of this GameObject*/
    private Rigidbody _rigidbody;
    /*If 'true' the object has been connected to another GameObject body*/
    private bool connected;
    private GameObject with;

    void Start()
    {
        base.Init();
        _rigidbody = GetComponent<Rigidbody>();
        //sets the right values of this Rigibody in order to perform a correct interaction
        _rigidbody.isKinematic = false;
        //must be enabled if the desired result is a falling object
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        connected = false;
    }

    /*Performs the interaction using the 'connected' variable value: if it's 'true' invokes the 'BreakConnection' method,
     *otherwise invokes the 'Connect' method*/
    public override void RealizeInteraction(GameObject obj)
    {
        if(!connected) { Connect(obj); }
        else { BreakConnection(obj); }
        Interact(connected);
    }

    /*Creates a FixedJoint between this body and the body which interacted with, setting the speed of that body
     *as 'weightedSpeed'. The connected body can't rotate horizontally and can't jump while is connected with this body*/
    private void Connect(GameObject with)
    {
        connected = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        FixedJoint fj = gameObject.AddComponent<FixedJoint>();
        this.with = with;
        fj.connectedBody = with.GetComponent<Rigidbody>();
        CharacterInput character = with.GetComponent<CharacterInput>();
        character.EnableJump(false);
        character.EnableCrouch(false);
        character.SetConnected(true);
        character.target.GetComponent<CameraController>().EnableRotation(false);
    }

    /*Destroys the FixedJoint between the two bodies and enables the rotation and the jump; it also resets the normal
     *speed of the connected body*/
    private void BreakConnection(GameObject with)
    {
        connected = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        this.with = null;
        Destroy(GetComponent<FixedJoint>());
        CharacterInput character = with.GetComponent<CharacterInput>();
        character.EnableJump(true);
        character.EnableCrouch(true);
        character.SetConnected(false);
        character.target.GetComponent<CameraController>().EnableRotation(true);
    }
}
