using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*MUST BE MERGED WITH THE NEW MOVEMENT SCRIPT*/
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterInput : MonoBehaviour
{
    public float speed;
    public float jumpSpeed = 8.0f;
    public float horizontalSpeed = 1.5f;
    public float gravity = -20f;

    public bool movementEnabled = true;
    public bool rotationEnabled = true;
    public bool jumpEnabled = true;
    public bool crouchEnabled = true;

    private const float distanceFromInteractable = 1.0f;
    private float _normalSpeed = 6.0f;

    private Vector3 movement = Vector3.zero;
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
            if(_charController.isGrounded)
            {
                /*Normal movement section*/
                float deltaZ = Input.GetAxis("Vertical") * speed;
                movement = new Vector3(0, 0, deltaZ);
                movement = transform.TransformDirection(movement);

                if (rotationEnabled)
                {
                    float deltaX = Input.GetAxis("Horizontal") * horizontalSpeed;
                    float rotationY = transform.localEulerAngles.y + deltaX;
                    transform.localEulerAngles = new Vector3(0, rotationY, 0);
                }

                /*Jump section*/
                if (Input.GetKeyDown(KeyCode.Space) && jumpEnabled)
                {
                    movement.y += jumpSpeed;
                }
            }

            movement.y += gravity * Time.deltaTime;
            _charController.Move(movement * Time.deltaTime);

            /*MISSING: Crouch section*/

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
    public void EnableJump(bool enabled) { jumpEnabled = enabled; }
    public void EnableCrouch(bool enabled) { crouchEnabled = enabled; }
    public void SetSpeed(float newSpeed) { speed = newSpeed; }
    public float normalSpeed
    {
        get { return _normalSpeed; }
    }
}
