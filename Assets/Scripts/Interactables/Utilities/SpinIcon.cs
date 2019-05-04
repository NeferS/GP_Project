﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinIcon : MonoBehaviour
{
    private float spinSpeed = 120.0f;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Rotate(new Vector3(0, spinSpeed, 0) * Time.deltaTime, Space.World);
    }
}