using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableObject : Interactable
{
    private Rigidbody _rigidbody;
    private bool connected;
    private const float weightedSpeed = 2.0f;

    void Start()
    {
        base.Init();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        _rigidbody.freezeRotation = true;
        connected = false;
    }


    void Update()
    {

    }

    public override void RealizeInteraction(GameObject obj)
    {
        if(!connected) { Connect(obj); }
        else { BreakConnection(obj); }
        Interact(connected);
    }

    private void Connect(GameObject with)
    {
        connected = true;
        FixedJoint fj = gameObject.AddComponent<FixedJoint>();
        fj.connectedBody = with.GetComponent<Rigidbody>();
        CharacterInput character = with.GetComponent<CharacterInput>();
        character.EnableRotation(false);
        character.SetSpeed(weightedSpeed);
    }

    private void BreakConnection(GameObject with)
    {
        connected = false;
        Destroy(GetComponent<FixedJoint>());
        CharacterInput character = with.GetComponent<CharacterInput>();
        character.EnableRotation(true);
        character.SetSpeed(character.normalSpeed);
    }
}
