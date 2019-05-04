using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Empty abstract class that defines the type of a Controller object. Used in the SceneInitializer to avoid 
 *errors caused by a misuse.*/
public abstract class Controller : MonoBehaviour
{
    public abstract void ExitTriggered();
}
