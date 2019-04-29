using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    [SerializeField] private GameObject _interactIcon;
    private bool interacted = false;

    void Start()
    {
        Init();
    }


    void Update()
    {
        
    }

    protected void Init() { _interactIcon.SetActive(false); }

    public void Activate(bool activate) { _interactIcon.SetActive(activate); }

    protected void Interact(bool interact)
    {
        interacted = interact;
        _interactIcon.SetActive(!interact);
    }

    public bool isInteracting() { return interacted; }

    public abstract void RealizeInteraction(GameObject obj);

}
