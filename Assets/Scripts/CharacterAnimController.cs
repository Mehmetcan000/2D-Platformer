using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    private Animator animator;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    public bool isAttacking = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayJumpingAnim()
    {
        animator.SetBool(IsJumping,true);
    }

    public void PlayIdleAnim()
    {
        animator.SetBool(IsRunning,false);
        animator.SetBool(IsJumping,false);
    }

    public void PlayRunningAnim()
    {
        animator.SetBool(IsJumping,false);
        animator.SetBool(IsRunning,true);
    }

    public void TriggerAttackingAnim()
    { 
        animator.SetTrigger("Attacking");
    }

    public void PlayDeadingAnim()
    {
        //Death
    }
}
