using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Settings")]
    public float mouseSensitivity = 10;
    public Transform target;
    public float distFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = 8f;
    public bool rotationEnabled = true;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    [Header("Speeds")]
    public float moveSpeed = 5;
    public float returnSpeed = 100;
    public float wallPush = 0.7f;

    [Header("Distances")]
    public float evenCloserDistanceToPlayer = 1;

    [Header("Mask")]
    public LayerMask collisionMask;

    private bool pitchLock = false;

    private void LateUpdate()
    {
        WallCheck();
        CollisionCheck(target.position - transform.forward * distFromTarget);

        if (!pitchLock)
        {
            if(rotationEnabled)
                yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);
        }
        else
        {
            if(rotationEnabled)
                yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch = pitchMinMax.y;

            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);
        }

        transform.eulerAngles = currentRotation;

        Vector3 e = transform.eulerAngles;
        e.x = 0;

        target.eulerAngles = e;
    }

    private void WallCheck()
    {

        Ray ray = new Ray(target.position, -target.forward);
        RaycastHit hit;


        if (Physics.SphereCast(ray, 0.2f, out hit, 0.7f, collisionMask))
        {
            pitchLock = true;
        }
        else
        {
            pitchLock = false;
        }

    }

    private void CollisionCheck(Vector3 retPoint)
    {
        RaycastHit hit;

        if (Physics.Linecast(target.position, retPoint, out hit, collisionMask))
        {

            Vector3 norm = hit.normal * wallPush;
            Vector3 p = hit.point + norm;

            moveSpeed = 1.7f;

            if (Vector3.Distance(Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime), target.position) > evenCloserDistanceToPlayer)
            {
                transform.position = Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime);
            }
            return;

        }

        
        moveSpeed = 5f;
        transform.position = retPoint;
        //transform.position = Vector3.Lerp(transform.position, retPoint, returnSpeed * Time.deltaTime);
        pitchLock = false;
    }

    public void EnableRotation(bool enabled) { rotationEnabled = enabled; }
}