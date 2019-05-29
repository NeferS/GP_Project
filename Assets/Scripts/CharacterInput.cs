using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterInput : MonoBehaviour
{
    public Transform target;
    Animator animator;
    public float rotSpeed = 6f;
    public float moveSpeed = 6f;
    public float jumpSpeed = 20.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -10f;

    public bool movementEnabled = true;
    public bool jumpEnabled = true;
    public bool crouchEnabled = true;
    public bool connected = false;

    private ControllerColliderHit _contact;
    private CharacterController _charController;
    private float _vertSpeed;
    private const float _normalSpeed = 6f;
    private bool side_Left = false;
    private bool side_Right = false;
    private bool Oblique_Forward_Right = false;
    private bool Oblique_Forward_Left = false;
    private bool Oblique_Back_Left = false;
    private bool Oblique_Back_Right = false;
    private bool Back = false;
    private bool BackCrouch = false;
    private bool Pushing = false;
    private bool Push = false, Pull = false;
    private bool GroundedAnimation = false;
    private AudioSource _soundSource;
    [SerializeField] AudioClip footStepSound;
    private float footStepSoundLength;
    private bool _step;



    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
        _step = true;
        footStepSoundLength = 0.30f;
        animator = GetComponentInChildren<Animator>();
        _soundSource = GetComponentInChildren<AudioSource>();
    }


    void Update()
    {

        if (_charController.velocity.magnitude > 1f && _step && _charController.isGrounded && 
            (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            _soundSource.PlayOneShot(footStepSound);
            StartCoroutine(WaitForFootSteps(footStepSoundLength));
        }

        Pushing = false;
        if (movementEnabled)
        {
            Vector3 movement = Vector3.zero;
            float horInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.Space) && !connected && crouchEnabled)
            {
                animator.SetBool("Crouch", true);

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !connected)
                {
                    animator.SetBool("Crouch_Oblique_Left_Walk", true);
                    Oblique_Forward_Left = true;
                    footStepSoundLength = 0.6f;

                }
                else
                {
                    animator.SetBool("Crouch_Oblique_Left_Walk", false);
                    Oblique_Forward_Left = false;

                }

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !connected)
                {

                    animator.SetBool("Crouch_Oblique_Right_Walk", true);
                    Oblique_Forward_Right = true;
                    footStepSoundLength = 0.6f;

                }
                else
                {
                    animator.SetBool("Crouch_Oblique_Right_Walk", false);
                    Oblique_Forward_Right = false;

                }

                if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !connected)
                {
                    animator.SetBool("Crouch_Right", true);
                    side_Right = true;
                    footStepSoundLength = 0.6f;
                }
                else
                {
                    animator.SetBool("Crouch_Right", false);
                    side_Right = false;
                }

                if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !connected)
                {
                    animator.SetBool("Crouch_Left", true);
                    side_Left = true;
                    footStepSoundLength = 0.6f;
                }
                else
                {
                    animator.SetBool("Crouch_Left", false);
                    side_Left = false;
                }

                if (Input.GetKey(KeyCode.W) && !Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.A) && !connected)
                {
                    animator.SetBool("Crouch_Forward", true);
                    footStepSoundLength = 0.6f;

                }
                else
                {
                    animator.SetBool("Crouch_Forward", false);

                }

                if (Input.GetKey(KeyCode.S) && !connected)
                {
                    animator.SetBool("Crouch_Back", true);
                    footStepSoundLength = 0.6f;
                    BackCrouch = true;

                }
                else
                {
                    animator.SetBool("Crouch_Back", false);
                    BackCrouch = false;
                }

            }
            else
            {
                animator.SetBool("Crouch", false);
                animator.SetBool("Crouch_Oblique_Left_Walk", false);
                animator.SetBool("Crouch_Oblique_Right_Walk", false);
                animator.SetBool("Crouch_Left", false);
                animator.SetBool("Crouch_Right", false);
                animator.SetBool("Crouch_Forward", false);
                animator.SetBool("Crouch_Back", false);

                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
                {
                    moveSpeed = 0f;
                    Pushing = true;
                }
                if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !connected)
                {
                    animator.SetBool("Walk_Left", true);
                    side_Left = true;
                    footStepSoundLength = 0.6f;
                }
                else
                {
                    animator.SetBool("Walk_Left", false);
                    side_Left = false;
                    footStepSoundLength = 0.3f;

                }

                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !connected)
                {
                    animator.SetBool("Walk_Right", true);
                    side_Right = true;
                    footStepSoundLength = 0.6f;
                }
                else
                {
                    animator.SetBool("Walk_Right", false);
                    side_Right = false;
                }

                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !connected)
                {
                    animator.SetBool("Oblique_Right_Back", true);
                    Oblique_Back_Right = true;
                    footStepSoundLength = 0.6f;

                }
                else
                {
                    Oblique_Back_Right = false;
                    animator.SetBool("Oblique_Right_Back", false);
                }



                if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !connected)
                {
                    animator.SetBool("Oblique_Left_Back", true);
                    Oblique_Back_Left = true;
                    footStepSoundLength = 0.6f;

                }
                else
                {
                    animator.SetBool("Oblique_Left_Back", false);
                    Oblique_Back_Left = false;

                }

                if ((!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && !connected)
                {
                    animator.SetBool("Back", true);
                    Back = true;
                    footStepSoundLength = 0.6f;

                }
                else
                {
                    animator.SetBool("Back", false);
                    Back = false;
                }



                if ((!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W) && !connected)
                {
                    animator.SetBool("Forward", true);
                    footStepSoundLength = 0.3f;
                }
                else
                    animator.SetBool("Forward", false);

                if (Input.GetKey(KeyCode.W) && connected)
                {
                    animator.SetBool("Push", true);
                    Push = true;
                    footStepSoundLength = 0.6f;
                }
                else
                {
                    animator.SetBool("Push", false);
                    Push = false;
                }

                if (Input.GetKey(KeyCode.S) && connected)
                {
                    animator.SetBool("Pull", true);
                    Pull = true;
                    footStepSoundLength = 0.6f;
                }
                else
                {
                    animator.SetBool("Pull", false);
                    Pull = false;
                }


                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
                {
                    animator.SetBool("Side_Right", true);
                    Oblique_Forward_Right = true;
                    footStepSoundLength = 0.6f;

                }
                else
                {
                    animator.SetBool("Side_Right", false);
                    Oblique_Forward_Right = false;

                }

                if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
                {
                    animator.SetBool("Side_Left", true);
                    Oblique_Forward_Left = true;
                    footStepSoundLength = 0.6f;
                }
                else
                {
                    animator.SetBool("Side_Left", false);
                    Oblique_Forward_Left = false;
                }
            
            }

            bool hitGround = false;
            RaycastHit hit;

            if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float check = (_charController.height + _charController.radius) / 1.9f;
                hitGround = hit.distance <= check;
            }
            if (hitGround)
            {

                animator.SetBool("Grounded", false);
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.C))
                {

                    if (Input.GetButtonDown("Jump") && !GroundedAnimation)
                    {
                        if (jumpEnabled)
                        {
                            moveSpeed = 0;
                            animator.SetBool("Jump", true);
                            GroundedAnimation = true;
                            StartCoroutine(wait());
                            _vertSpeed = jumpSpeed;
                        }
                    }
                    else
                    {
                        //moveSpeed = normalSpeed;
                        animator.SetBool("Grounded", true);
                        animator.SetBool("Jump", false);
                        _vertSpeed = minFall;
                    }
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
            if (_charController.isGrounded && GroundedAnimation)
            {
                moveSpeed = 0f;
            }
            else
            {
                if (!Pushing && !connected)
                {
                    if (side_Left || side_Right || Oblique_Back_Left || Oblique_Back_Right || Oblique_Forward_Left || Oblique_Forward_Right || Back || BackCrouch)
                        moveSpeed = 3f;
                    else
                    {
                        moveSpeed = normalSpeed;
                    }

                }

                if (Pull || Push)
                {
                    moveSpeed = 7f;
                }

            }

            if (horInput != 0 || vertInput != 0)
            {
                if (!connected && !BackCrouch && _charController.isGrounded)
                    movement.x = horInput * moveSpeed;
        
                movement.z = vertInput * moveSpeed;
                movement = Vector3.ClampMagnitude(movement, moveSpeed);
                movement = target.TransformDirection(movement);
            }

            movement.y = _vertSpeed;

            movement *= Time.deltaTime;
            _charController.Move(movement);

        }
    }

    IEnumerator WaitForFootSteps(float stepsLength)
    {
        _step = false;
        yield return new WaitForSeconds(stepsLength);
        _step = true;
    }

    private IEnumerator wait()
    {

        yield return new WaitForSeconds(1.3f);
        GroundedAnimation = false;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }

    public void EnableMovement(bool enabled) { movementEnabled = enabled; }
    public void EnableJump(bool enabled) { jumpEnabled = enabled; }
    public void EnableCrouch(bool enabled) { crouchEnabled = enabled; }
    public void SetConnected(bool connected) { this.connected = connected; }
    public float normalSpeed
    {
        get { return _normalSpeed; }
    }
}
