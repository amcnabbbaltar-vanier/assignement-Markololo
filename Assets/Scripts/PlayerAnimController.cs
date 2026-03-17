using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private Rigidbody rb;
    // public AudioSource source; 

    public void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
        // source = GetComponent<AudioSource>();
    }

    public void Update()
    {
        animator.SetFloat("CharacterSpeed", rb.linearVelocity.magnitude);
        animator.SetBool("IsGrounded", movement.IsGrounded);
       
        if (Input.GetButtonDown("Jump") && movement.IsGrounded)
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetButtonDown("Jump") && !movement.IsGrounded && movement.IsJumpBoosted)
        {
            animator.SetTrigger("Flip"); 
        }
    }
}


