using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class defines the behaviour of a pressure pad by its animator.*/
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class PressurePad : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*Pushes down the pressure pad and changes its color*/
    void OnTriggerEnter()
    {
        animator.SetBool("Activate", true);
    }

    /*Pulles up the pressure pad and restores its original color*/
    void OnTriggerExit()
    {
        animator.SetBool("Activate", false);
    }
}
