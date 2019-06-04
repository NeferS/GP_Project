using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 1.5f;
    private float _rotY;
    private float _rotX;
    private Vector3 _offset;
    private float maximumVert = 45f;
    private float minimumvert = -45f;
    private bool rotationEnabled = true;
    
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _rotX = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }

    private void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Horizontal");
        Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
        transform.position = target.position - (rotation * _offset);

        if (horInput != 0 || verInput != 0)
        {
            if(rotationEnabled)
                _rotY += horInput * rotSpeed;
        }
        else
        {
            if(rotationEnabled && target.GetComponent<CharacterController>().isGrounded)
                _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
            _rotX -= Input.GetAxis("Mouse Y") * rotSpeed * 3;
            _rotX = Mathf.Clamp(_rotX, minimumvert, maximumVert);

        }


        transform.LookAt(target);


    }

    public void EnableRotation(bool value)
    {
        rotationEnabled = value;
    }

    public bool getEnableRotation()
    {
        return rotationEnabled;
    }
}
