using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterInput : MonoBehaviour
{
    public float speed;
    public float horizontalSpeed = 1.5f;
    public float gravity = -9.8f;

    public bool movementEnabled = true;
    public bool rotationEnabled = true;
    public bool jumpcrouchEnabled = true;

    private const float distanceFromInteractable = 1.0f;
    private float _normalSpeed = 6.0f;

    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        speed = _normalSpeed;
    }


    void Update()
    {
        if (movementEnabled)
        {
            /*Normal movement section*/
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(0, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = gravity;

            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);

            if(rotationEnabled)
            {
                float deltaX = Input.GetAxis("Horizontal") * horizontalSpeed;
                float rotationY = transform.localEulerAngles.y + deltaX;
                transform.localEulerAngles = new Vector3(0, rotationY, 0);
            }

            /*MISSING: Jump and crouch section*/

            /*Interaction with scene element section*/
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            GameObject hitObject = null;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                hitObject = hit.transform.gameObject;
                Interactable interactable = hitObject.GetComponent<Interactable>();
                if (interactable != null && interactable.enabled)
                {
                    if (hit.distance <= distanceFromInteractable && !interactable.isInteracting())
                    {
                        hitObject.GetComponent<Interactable>().Activate(true);
                    }
                    else if(hit.distance > distanceFromInteractable) { hitObject.GetComponent<Interactable>().Activate(false); }
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && hitObject != null && hit.distance <= distanceFromInteractable)
            {
                hitObject.GetComponent<Interactable>().RealizeInteraction(gameObject);
            }
        }
    }

    public void EnableMovement(bool enabled) { movementEnabled = enabled; }
    public void EnableRotation(bool enabled) { rotationEnabled = enabled; }
    public void EnableJumpAndCrouch(bool enabled) { jumpcrouchEnabled = enabled; }
    public void SetSpeed(float newSpeed) { speed = newSpeed; }
    public float normalSpeed
    {
        get { return normalSpeed; }
    }
}
