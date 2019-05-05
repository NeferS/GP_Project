using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of 'SceneInitializer'.*/
[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(SceneLoader))]
public class BeforeControllerInitializer : SceneInitializer
{
    void Start()
    {
        GetComponent<SceneLoader>().enabled = false;
        base.Reset();
    }
    /*Just sets some default values on the variables of the 'SceneLoader' object, enables it, enables the Controller
     *and disables itself, considering its work finished*/
    public override void DoAction()
    {
        GetComponent<Controller>().enabled = true;
        SceneLoader sl = GetComponent<SceneLoader>();
        sl.Reset();
        sl.enabled = true;
        enabled = false;
    }
}
