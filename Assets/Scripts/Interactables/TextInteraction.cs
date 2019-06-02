using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This class defines the behaviour of the objects that interact with a Text component in a Canvas.*/
public class TextInteraction : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private GameObject target;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>() && text != null)
        {
            target.SetActive(true);
            target.GetComponent<Text>().text = text;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<CharacterController>())
        {
            target.GetComponent<Text>().text = "";
            target.SetActive(false);
        }
    }
}
