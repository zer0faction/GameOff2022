using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor, IInputReader
{
    [Header("Actor subclass player: references to other GameObjects")]
    [SerializeField] private RenderDirectionSetter renderDirectionSetter;

    [Header("Actor subclass player: Jumping stats")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float minJumpHeight;
    [SerializeField] private float timeToReachJumpApex;
    [SerializeField] private float gravityMultiplierOnFallingDown = 1.5f;

    [Header("Actor subclass player: Timers")]
    [SerializeField] private float jumpButtonPressedTime = .1f;
    [SerializeField] private float coyoteTime = .1f;
    private float jumpButtonPressedTimer;
    private float coyoteTimer;

    private float jumpPower;
    private float minJumpPower;
    private bool reachedApex = true;
    private float maxHeightReached = Mathf.NegativeInfinity;
    private bool jumpButtonStillPressed;
    private float gravityMultiplier = 1f;

    private void Start()
    {
        initialGravity = -2 * jumpHeight / (timeToReachJumpApex * timeToReachJumpApex);
        jumpPower = 2 * jumpHeight / timeToReachJumpApex;
        minJumpPower = 2 * minJumpHeight / timeToReachJumpApex;
        defaultMoveAcceleration = maxDefaultMovingSpeed / timeTillFullDefaultMovingSpeed;
        defaultMoveDeceleration = maxDefaultMovingSpeed / timeTillFullStop;
    }

    public void InputData(InputData inputData)
    {
        deltaPosition = new Vector3(0, 0, 0);
        if (currentYSpeed < 0) { gravityMultiplier = gravityMultiplierOnFallingDown; } else { gravityMultiplier = 1; }
        ApplyDefaultHorizontalMovement(inputData.x);

        if (inputData.jumpButtonReleased) { OnJumpButtonReleased(); }
        if (inputData.jumpButtonPressed) { OnJumpButtonPressed(); }

        JumpButtonPressedTimer();
        CoyoteTimer();

        CheckIfShouldJump();
        CheckIfApexReached();

        ApplyGravity(gravityMultiplier);

        if (IsBumpingHead())
        {
            currentYSpeed = 0;
        }

        deltaPosition = GetNewDeltaPosition();

        if(mover2D.collisions.left || mover2D.collisions.right)
            currentXSpeed = 0;

        mover2D.Move(deltaPosition);
        if (IsGrounded()) { currentYSpeed = 0; }
        renderDirectionSetter.SetDir(GetCurrentDir());
    }

    public override void ApplyDefaultHorizontalMovement(int inputDirection)
    {
        if (inputDirection == 0) { ApplyFrictionX(1, 0.5f); }
        else
        {

            if (Mathf.Abs(currentXSpeed) > maxDefaultMovingSpeed)
            {
                //ApplyFrictionX(1,2f);
                currentXSpeed = maxDefaultMovingSpeed * GetCurrentDir();
            }
            else
            { currentXSpeed += inputDirection * defaultMoveAcceleration * Time.deltaTime; }
        }
    }

    private void OnJumpButtonPressed()
    {
        jumpButtonStillPressed = true;
        jumpButtonPressedTimer = jumpButtonPressedTime;
    }

    private void OnJumpButtonReleased()
    {
        if (currentYSpeed > minJumpPower)
        {
            currentYSpeed = minJumpPower;
        }
    }

    private void CheckIfShouldJump()
    {
        if ((coyoteTimer > 0 && jumpButtonStillPressed))
        {
            Jump();
            coyoteTimer = -coyoteTime;
        }
    }

    private void JumpButtonPressedTimer()
    {
        if (jumpButtonPressedTimer > 0)
        {
            jumpButtonPressedTimer -= Time.deltaTime;
        }
        else
        {
            jumpButtonStillPressed = false;
        }
    }

    private void CheckIfApexReached()
    {
        if (!reachedApex && maxHeightReached > transform.position.y) //. Vertical movement. 
        {
            reachedApex = true;
        }
    }

    private void CoyoteTimer()
    {
        if (IsGrounded())
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            if (coyoteTimer > 0)
            {
                coyoteTimer -= Time.deltaTime;
            }
        }
    }

    private void Jump()
    {
        maxHeightReached = Mathf.NegativeInfinity;
        currentYSpeed = jumpPower;
        reachedApex = false;
    }
}
