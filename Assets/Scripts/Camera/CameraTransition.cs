using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*UNUSED*/
public class CameraTransition : MonoBehaviour
{

    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _fieldCamera;

    private const float finalRotationX = 27.0f;
    private const float movementPerStep = 0.1f;
    private const float rotationPerStep = 0.2f;
    private bool begin = false, goToStart = false;

    void Start()
    {

    }


    void Update()
    {
        if(begin)
        {
            float newXrotation = (transform.localEulerAngles.x < finalRotationX) ? transform.localEulerAngles.x + rotationPerStep :
                                                                                   transform.localEulerAngles.x;

            if(transform.position.y < _fieldCamera.transform.position.y)
                transform.Translate(new Vector3(0.0f, movementPerStep, 0.0f), Space.Self);
            if(Mathf.Abs(transform.position.z) < Mathf.Abs(_fieldCamera.transform.position.z))
                transform.Translate(new Vector3(0.0f, 0.0f, -movementPerStep), Space.Self);
            transform.localEulerAngles = new Vector3(newXrotation, transform.localEulerAngles.y, transform.localEulerAngles.y);

            if (transform.position.y >= _fieldCamera.transform.position.y && 
                Mathf.Abs(transform.position.z) >= Mathf.Abs(_fieldCamera.transform.position.z))
            {
                _fieldCamera.SetActive(true);
                _fieldCamera.GetComponent<CameraLook>().enabled = true;
                _fieldCamera.GetComponent<CharacterController>().enabled = true;
                GetComponent<Camera>().enabled = false;
                begin = false;
            }
        }
    }

    public void Begin()
    {
        _mainCamera.SetActive(false);
        GetComponent<Camera>().enabled = true;
        begin = true;
    }
    public void Return()
    {
        
        goToStart = true;
    }

}
