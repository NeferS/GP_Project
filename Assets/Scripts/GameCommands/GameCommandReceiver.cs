using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used to call the proper GameCommandHandler subclass to a given GameCommandType received from a subclass of SendGameCommand
/*IMPORTED FROM THE '3DGamekit' FREE ASSETS IN THE UNITY STORE. This script has been modified in order to fit the needs of project.
 *In particular, the original Dictionary stored a List<System.Action>, which was too generic. After the modifications, the action is
 *specific and receives a GameCommandType as parameter, in order to propagate the type of command received to the handler.*/
public class GameCommandReceiver : MonoBehaviour
{
    Dictionary<GameCommandType, List<System.Action<GameCommandType>>> handlers = 
                new Dictionary<GameCommandType, List<System.Action<GameCommandType>>>();
    /*Invokes the previously registered method on the GameCommandHandler*/
    public void Receive(GameCommandType e)
    {
        List<System.Action<GameCommandType>> callbacks = null;
        if (handlers.TryGetValue(e, out callbacks))
        {
            foreach (var i in callbacks) i(e);
        }
    }
    /*Registers a specific method of the GameCommandHandler, which will be invoked asynchronously*/
    public void Register(GameCommandType type, GameCommandHandler handler)
    {
        List<System.Action<GameCommandType>> callbacks = null;
        if (!handlers.TryGetValue(type, out callbacks))
        {
            callbacks = handlers[type] = new List<System.Action<GameCommandType>>();
        }
        callbacks.Add(handler.OnInteraction);
    }
    /*Removes a previously registered method from the list of actions relative to a GameCommandHandler*/
    public void Remove(GameCommandType type, GameCommandHandler handler)
    {
        handlers[type].Remove(handler.OnInteraction);
    }
}
