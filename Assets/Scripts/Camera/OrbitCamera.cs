using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 1.5f;
    private float _rotY;
    private float _rotX;
    private Vector3 _offset;
    public float minVert = -45.0f;
    public float maxVert = 45.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _rotX = transform.eulerAngles.x;
        _offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
        transform.position = target.position - (rotation * _offset);

        if(horInput != 0)
        {
            _rotY += horInput * rotSpeed;
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
            _rotX -= Input.GetAxis("Mouse Y") * rotSpeed * 3;
            _rotX = Mathf.Clamp(_rotX, minVert, maxVert);
            

        }

        transform.LookAt(target);

        
    }

}
