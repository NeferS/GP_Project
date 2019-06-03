using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*IMPORTED FROM LESSON*/
public interface IGameManager
{
    ManagerStatus status { get; }

    void Startup();
}
