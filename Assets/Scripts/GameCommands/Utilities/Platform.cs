using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{
    [SerializeField]
    protected CharacterController m_CharacterController;

    void OnTriggerStay(Collider other)
    {
        CharacterController character = other.GetComponent<CharacterController>();

        if (character != null)
            m_CharacterController = character;

    }

    void OnTriggerExit(Collider other)
    {
        if (m_CharacterController != null && other.gameObject == m_CharacterController.gameObject)
            m_CharacterController = null;
    }

    public void MoveCharacterController(Vector3 deltaPosition)
    {
        if (m_CharacterController != null)
        {
            m_CharacterController.Move(deltaPosition);
        }
    }
}