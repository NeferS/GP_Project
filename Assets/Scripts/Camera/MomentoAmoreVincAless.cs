using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentoAmoreVincAless : MonoBehaviour
{

    void Start()
    {
        float cos = Vector3.Dot(new Vector3(0, 1, 0), new Vector3(0, 12.3f, -17).normalized);
        float angle = Mathf.Acos(cos);
        Debug.Log(90 - ((angle * 180) / Mathf.PI));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
