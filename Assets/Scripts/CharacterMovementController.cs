using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public enum MovementStates
    {
        Idle,
        Running,
        Jumping,
        Attacking,
        Death
    }
    
    public enum  FacingDirection
    {
        Right,
        Left
    }

    private CharacterAnimController animController;
    
    [HideInInspector]
    public  Rigidbody2D playerRigidbody;
    
    private SpriteRenderer playerSprite;

  
    
    [Header("Movement Values")]
    public float movementSpeed; 
    public float playerJumpForce;
    
    [Header("Movement States")]
    public MovementStates characterMovementStates;
    public FacingDirection facingDirection;
    
    [Header("Raycast Value and Mask")]
    public float isGroundedRayLength;
    public  LayerMask platformLayerMask;
    
    
 


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animController = GetComponent<CharacterAnimController>();

    }


    void Update()
    {
        HandleJump();
    }
    private void FixedUpdate()
    {
        HandleMovement();
        PlayAnimState();
        SetPlayerDirection();
    }

    
    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            playerRigidbody.velocity = Vector2.up * playerJumpForce; 
        }
    }
    
    public bool IsGrounded()
    {
        var bounds = playerSprite.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bounds.center,
            bounds.size,0f,Vector2.down,isGroundedRayLength,platformLayerMask);
        
        if (raycastHit2D.collider !=null)
        {
            Debug.Log("We are colliding with" + raycastHit2D.collider);
        }
        
        return raycastHit2D.collider != null;
    }
    
    private void HandleMovement()
    {
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKey(KeyCode.A))
        {
            playerRigidbody.velocity = new Vector2(-movementSpeed, playerRigidbody.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                playerRigidbody.velocity = new Vector2(movementSpeed, playerRigidbody.velocity.y);
            }
            else
            {
                playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
                playerRigidbody.constraints =
                    RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
    
   
    
    private void SetPlayerDirection()
    {
        switch (facingDirection)
        {
            case FacingDirection.Right:
                playerSprite.flipX = false;
                break;
            case FacingDirection.Left:
                playerSprite.flipX = true;
                break;
            default:
                break;
        }
    }
    
    
    private void PlayAnimState()
    {
        switch (characterMovementStates)
        {
            case MovementStates.Idle:
                animController.PlayIdleAnim();
                break;
            case MovementStates.Running:
               animController.PlayRunningAnim();
                break;
            case MovementStates.Jumping:
             animController.PlayJumpingAnim();
                break;
            case MovementStates.Attacking:
                break;
            case MovementStates.Death:
                break;
            default:
                break;
        }
    }


    public void SetMovementState(MovementStates movementStates)
    {
        characterMovementStates = movementStates;
    }


}
