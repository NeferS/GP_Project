using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*UNUSED*/
public class CameraLook : MonoBehaviour
{

    public float speed = 4.0f;
    public float verticalSensitivity = 9.0f;

    public float minVert = -45.0f;
    public float maxVert = 45.0f;

    private float _rotationX = 0;

    void Start()
    {
        
    }


    void Update()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * verticalSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

        transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
    }
}
