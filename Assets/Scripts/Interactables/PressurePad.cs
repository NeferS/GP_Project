using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class PressurePad : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter()
    {
        animator.SetBool("Activate", true);
    }

    void OnTriggerExit()
    {
        animator.SetBool("Activate", false);
    }
}
