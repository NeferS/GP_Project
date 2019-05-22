using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Controller
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        
    }

    public override void ExitTriggered()
    {
        throw new System.NotImplementedException();
    }
}
