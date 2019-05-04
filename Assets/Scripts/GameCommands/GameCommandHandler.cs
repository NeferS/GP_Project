using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class need to be subclassed to implement behaviour based on receiving game command 
// (see class in SwitchMaterial.cs or PlaySound.cs for sample)
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

    protected virtual void Awake()
    {
        GameCommandReceiver receiver = GetComponent<GameCommandReceiver>();
        foreach (GameCommandType interaction in interactionsType)
            receiver.Register(interaction, this);
    }
}