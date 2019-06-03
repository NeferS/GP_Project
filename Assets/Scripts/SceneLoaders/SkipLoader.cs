using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This simple script immediately loads the scene pointed by the 'sceneIndex' in the SceneLoader if 'Enter' is pressed.*/
[RequireComponent(typeof(SceneLoader))]
public class SkipLoader : MonoBehaviour
{
    private bool pressed = false;
    public int sceneIndex = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !pressed)
        {
            pressed = false;
            GetComponent<SceneLoader>().enabled = true;
            GetComponent<SceneLoader>().Load(sceneIndex);
        }
    }
}
