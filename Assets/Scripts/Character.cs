using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    private CharacterMovementController _movementController;
    private CharacterAnimController _characterAnimController;
    private CharacterCombat _characterCombat;
    private Health _health;
    
    private void Awake()
    {
        _movementController = GetComponent<CharacterMovementController>();
        _characterAnimController = GetComponent<CharacterAnimController>();
        _characterCombat = GetComponent<CharacterCombat>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _movementController.characterMovementStates !=
            CharacterMovementController.MovementStates.Jumping)
        {
            StartCoroutine(AttackOrder());
        }
    }


    private void FixedUpdate()
    {
        SetCharacterState();

      
    }
    
    private void SetCharacterState()
    {
        if (_characterCombat.isAttacking)
        {
            return;
        }
        
        if (_movementController.IsGrounded()) // We are on the ground.
        {
            if (_movementController.playerRigidbody.velocity.x == 0) // Are we standing ?
            {// play IDLE Anim
                _movementController.SetMovementState(CharacterMovementController.MovementStates.Idle);
            }
            else if (_movementController.playerRigidbody.velocity.x > 0 )
            { //Play RUN Anim +X
                _movementController.facingDirection = CharacterMovementController.FacingDirection.Right;
                _movementController.SetMovementState(CharacterMovementController.MovementStates.Running);
            }
            else if (_movementController.playerRigidbody.velocity.x < 0)
            {//Play RUN Anim -X
                _movementController.facingDirection = CharacterMovementController.FacingDirection.Left;
                _movementController.SetMovementState(CharacterMovementController.MovementStates.Running);
            }
        }
        else
        {  // Play JUMP Anim
            _movementController.SetMovementState(CharacterMovementController.MovementStates.Jumping);
        }
    }

    private IEnumerator AttackOrder()
    {
        if (_characterCombat.isAttacking)
        {
          yield break;
        }
        _characterCombat.isAttacking = true;

        _movementController.characterMovementStates = CharacterMovementController.MovementStates.Attacking;
        _characterAnimController.TriggerAttackingAnim();

        yield return new WaitForSeconds(.8f);
        
        _characterCombat.Attack();

        _movementController.characterMovementStates = CharacterMovementController.MovementStates.Idle;

        _characterCombat.isAttacking = false;
        yield break;
    }
}
