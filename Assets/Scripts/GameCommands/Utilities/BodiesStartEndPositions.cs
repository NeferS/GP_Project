using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Simple rapresentation of the 'start' and 'end' transform positions of a GameObject. Useful when the object has to be moved
 *by, for example, a 'MultipleTranslator'.*/
/*if MultipleTranslator is UNUSED -> UNUSED*/
[RequireComponent(typeof(Rigidbody))]
public class BodiesStartEndPositions : MonoBehaviour 
{ 
    public Vector3 start;
    public Vector3 end;

}
