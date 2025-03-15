using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement characterMovement;
    private Rigidbody rb;

    public void Start()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
        CharacterMovement.OnDoubleJump += DoFlip;
    }

    void OnDestroy()
    {
        CharacterMovement.OnDoubleJump -= DoFlip;
    }

    public void LateUpdate()
    {
       UpdateAnimator();
    }

    void UpdateAnimator()
    {
        animator.SetFloat("CharacterSpeed", rb.velocity.magnitude);
        animator.SetBool("IsGrounded", characterMovement.IsGrounded);
    }
    
    void DoFlip()
    {
        animator.SetTrigger("doFlip");
    }
}
