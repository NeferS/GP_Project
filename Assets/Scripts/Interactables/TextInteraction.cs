using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This class is a subclass of Interactable and defines the behaviour of the objects that interact with a Text component in a Canvas.*/
public class TextInteraction : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private Text target;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>() && text != null)
        {
            target.enabled = true;
            target.text = text;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<CharacterController>())
        {
            target.enabled = false;
            target.text = "";
        }
    }
}
