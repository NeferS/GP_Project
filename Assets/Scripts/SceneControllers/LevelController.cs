using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of Controller; it's a simple controller for the level scene.*/
public class LevelController : Controller
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void ExitTriggered()
    {
        throw new System.NotImplementedException();
    }
}
