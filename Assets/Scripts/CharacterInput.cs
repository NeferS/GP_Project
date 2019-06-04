using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterInput : MonoBehaviour
{
    public Transform target;
    private Animator anim;
    public float rotSpeed = 15.0f;
    public float moveSpeed = 8.0f;
    private CharacterController _charController;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;
    private bool GroundedAnimation = false;
    private ControllerColliderHit _contact;
    private AudioSource _soundSource;
    [SerializeField] AudioClip footStepSound;
    private float footStepSoundLength;
    private bool _step;
    private bool Push = false, Pull = false;
    private bool movementEnabled = true;
    private bool jumpEnabled = true;
    public bool connected = false;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
        _step = true;
        footStepSoundLength = 0.3f;
        anim = GetComponentInChildren<Animator>();
        _soundSource = GetComponentInChildren<AudioSource>();
    }
    
    void Update()
    {
        if(movementEnabled)
        {
            Vector3 movement = Vector3.zero;
            float horInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");

            if (_charController.velocity.magnitude > 1f && _step && _charController.isGrounded && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
            {
                _soundSource.PlayOneShot(footStepSound);
                StartCoroutine(WaitForFootSteps(footStepSoundLength));
            }

            if (connected)
                footStepSoundLength = 1.15f;
            else
                footStepSoundLength = 0.3f;

            if (Input.GetKey(KeyCode.W) && connected)
            {
                anim.SetBool("Push", true);
                Push = true;
            }
            else
            {
                anim.SetBool("Push", false);
                Push = false;
            }

            if (Input.GetKey(KeyCode.S) && connected)
            {
                anim.SetBool("Pull", true);
                Pull = true;
            }
            else
            {
                anim.SetBool("Pull", false);
                Pull = false;
            }


            if (_charController.isGrounded && GroundedAnimation)
                moveSpeed = 0f;
            else
            {
                if (connected && (Pull || Push))
                    moveSpeed = 3f;
                else
                    moveSpeed = 8f;
            }

            if (horInput != 0 || vertInput != 0)
            {
                if (_charController.isGrounded && target.GetComponent<CameraController>().getEnableRotation())
                    movement.x = horInput * moveSpeed;
                movement.z = vertInput * moveSpeed;
                movement = Vector3.ClampMagnitude(movement, moveSpeed);

                Quaternion tmp = target.rotation;
                target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
                movement = target.TransformDirection(movement);
                target.rotation = tmp;
                Quaternion direction = Quaternion.LookRotation(movement);

                if (!GroundedAnimation && target.GetComponent<CameraController>().getEnableRotation())
                    transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
                if (!connected)
                    anim.SetBool("Forward", true);
            }
            else
                anim.SetBool("Forward", false);


            bool hitGround = false;
            RaycastHit hit;

            if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float check = (_charController.height + _charController.radius) / 1.9f;
                hitGround = hit.distance <= check;
            }



            if (hitGround)
            {
                anim.SetBool("Grounded", false);
                if (Input.GetButtonDown("Jump") && !GroundedAnimation)
                {
                    if (jumpEnabled)
                    {
                        _vertSpeed = jumpSpeed;
                        GroundedAnimation = true;
                        StartCoroutine(wait());
                    }

                }
                else
                {
                    _vertSpeed = minFall;
                    anim.SetBool("Jump", false);
                    if(_charController.isGrounded)
                        anim.SetBool("Grounded", true);
                }
            }
            else
            {
                _vertSpeed += gravity * 5 * Time.deltaTime;
                if (_vertSpeed < terminalVelocity)
                    _vertSpeed = terminalVelocity;

                anim.SetBool("Jump", true);
                if (_charController.isGrounded)
                {
                    if (Vector3.Dot(movement, _contact.normal) < 0)
                        movement = _contact.normal * moveSpeed;
                    else
                        movement += _contact.normal * moveSpeed;
                }
            }
            
            movement.y = _vertSpeed;

            movement *= Time.deltaTime;
            _charController.Move(movement);
        }
    }
    private IEnumerator wait()
    {

        yield return new WaitForSeconds(1.3f);
        GroundedAnimation = false;

    }
    IEnumerator WaitForFootSteps(float stepsLength)
    {
        _step = false;
        yield return new WaitForSeconds(stepsLength);
        _step = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }
    public void SetConnected(bool connected) { this.connected = connected; }
    public void EnableMovement(bool enabled) { movementEnabled = enabled; }
    public void EnableJump(bool enabled) { jumpEnabled = enabled; }
}
