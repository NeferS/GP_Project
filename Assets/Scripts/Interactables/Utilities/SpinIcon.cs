using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Simple script that rotates an object. Used to rotate the interaction icon (the 'E' model).*/
public class SpinIcon : MonoBehaviour
{
    private float spinSpeed = 120.0f;
  
    void Update()
    {
        transform.Rotate(new Vector3(0, spinSpeed, 0) * Time.deltaTime, Space.World);
    }
}
