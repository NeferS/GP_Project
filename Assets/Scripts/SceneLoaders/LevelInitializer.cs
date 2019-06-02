using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This class is a subclass of 'SceneInitializer'. Basically, it destroyes the Image component from the gui and disables itself
 *after it fades.*/
public class LevelInitializer : SceneInitializer
{ 
    void Start()
    {
        GetComponent<SceneLoader>().enabled = false;
        base.Reset();
    }

    public override void DoAction()
    {
        Destroy(_gui.GetComponent<Image>());
        enabled = false;
    }
}
