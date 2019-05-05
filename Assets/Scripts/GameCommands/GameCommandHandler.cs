using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class need to be subclassed to implement behaviour based on receiving game command 
// (see class in SwitchMaterial.cs or PlaySound.cs for sample)
/*IMPORTED FROM THE '3DGamekit' FREE ASSETS IN THE UNITY STORE. This script has been modified in order to fit the needs of project.
 *In particular, the original script uses just one GameCommandType instead of an array and don't need any parameter in the methods;
 *moreover, for this reason, it used the 'Invoke(string methodName, float time)' in the 'ExecuteInteraction' method instead of 
 *'StartCoroutine(IEnumerator routine)'. Because of an array of GameCommandType, the handler must know which GameCommand has to be
 *executed and for this reason the methods receive the propagation of the GameCommand from the receiver.*/
[SelectionBase]
[RequireComponent(typeof(GameCommandReceiver))]
public abstract class GameCommandHandler : MonoBehaviour
{
    public GameCommandType[] interactionsType = new GameCommandType[1];
    public bool isOneShot = false;
    public float coolDown = 0;
    public float startDelay = 0;

    protected bool isTriggered = false;

    float startTime = 0;

    public abstract void PerformInteraction(GameCommandType type);

    /*This routine is invoked if the startDelay is greater than 0; basically waits for a startDelay time and invokes
     *the 'PerformInteraction' method*/
    protected virtual IEnumerator DelayedPerformInteraction(GameCommandType type)
    {
        yield return new WaitForSeconds(startDelay);
        PerformInteraction(type);
    }

    [ContextMenu("Interact")]
    public virtual void OnInteraction(GameCommandType type)
    {
        if (isOneShot && isTriggered) return;
        isTriggered = true;
        if (coolDown > 0)
        {
            if (Time.time > startTime + coolDown)
            {
                startTime = Time.time + startDelay;
                ExecuteInteraction(type);
            }
        }
        else
            ExecuteInteraction(type);
    }

    void ExecuteInteraction(GameCommandType type)
    {
        if (startDelay > 0)
            StartCoroutine(DelayedPerformInteraction(type));
        else
            PerformInteraction(type);
    }
    /*The original script registered just the single GameCommandType; after the modifications, of course, the entire array
     *is registered to the GameCommandReceiver*/
    protected virtual void Awake()
    {
        GameCommandReceiver receiver = GetComponent<GameCommandReceiver>();
        foreach (GameCommandType interaction in interactionsType)
            receiver.Register(interaction, this);
    }
}