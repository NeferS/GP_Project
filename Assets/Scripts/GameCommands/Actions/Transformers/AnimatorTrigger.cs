using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorTrigger : GameCommandHandler
{
    [SerializeField] private AudioClip clip;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public override void PerformInteraction(GameCommandType type)
    {
        if (type == GameCommandType.Activate)
        {
            if (audioSource && clip)
                audioSource.PlayOneShot(clip);
            animator.SetTrigger("Open");
        }
    }
}
