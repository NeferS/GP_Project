using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private Transform target;
    Animator animator;
    public float rotSpeed = 6f;
    public float moveSpeed = 6f;
    public float jumpSpeed = 20.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -10f;

    public bool movementEnabled = true;
    public bool rotationEnabled = true;
    public bool jumpEnabled = true;
    public bool crouchEnabled = true;

    private ControllerColliderHit _contact;
    private CharacterController _charController;
    private float _vertSpeed;
    private const float _normalSpeed = 6f;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if(movementEnabled)
        {
            Vector3 movement = Vector3.zero;
            float horInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");

            if (horInput != 0 || vertInput != 0)
            {
                if (rotationEnabled)
                    movement.x = horInput * moveSpeed;
                movement.z = vertInput * moveSpeed;
                movement = Vector3.ClampMagnitude(movement, moveSpeed);
                Quaternion tmp = target.rotation;
                target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
                movement = target.TransformDirection(movement);
                target.rotation = tmp;
                Quaternion direction = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                animator.SetBool("Back", true);
            else
                //if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                animator.SetBool("Back", false);



            //animator.SetFloat("Speed", vertInput);
            bool hitGround = false;
            RaycastHit hit;

            if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float check = (_charController.height + _charController.radius) / 1.9f;
                hitGround = hit.distance <= check;
            }
            if (hitGround)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    if(jumpEnabled)
                    {
                        animator.SetBool("Jump", true);
                        _vertSpeed = jumpSpeed;
                    }
                }
                else
                {
                    animator.SetBool("Jump", false);
                    _vertSpeed = minFall;
                }
            }
            else
            {
                _vertSpeed += gravity * 5 * Time.deltaTime;
                if (_vertSpeed < terminalVelocity)
                {
                    _vertSpeed = terminalVelocity;
                }

                if (_charController.isGrounded)
                {
                    if (Vector3.Dot(movement, _contact.normal) < 0)
                    {
                        movement = _contact.normal * moveSpeed;
                    }
                    else
                    {
                        movement += _contact.normal * moveSpeed;
                    }
                }
            }

            movement.y = _vertSpeed;

            movement *= Time.deltaTime;
            _charController.Move(movement);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }

    public void EnableMovement(bool enabled) { movementEnabled = enabled; }
    public void EnableRotation(bool enabled) { rotationEnabled = enabled; }
    public void EnableJump(bool enabled) { jumpEnabled = enabled; }
    public void EnableCrouch(bool enabled) { crouchEnabled = enabled; }
    public void SetSpeed(float newSpeed) { moveSpeed = newSpeed; }
    public float normalSpeed
    {
        get { return _normalSpeed; }
    }
}
