using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [Header("Actor superclass: references to other GameObjects")]
    public Mover2D mover2D;

    [Header("Actor superclass: Walking stats")]
    public float maxDefaultMovingSpeed;
    public float timeTillFullDefaultMovingSpeed;
    public float timeTillFullStop;

    [Header("Actor superclass: Walking stats")]
    public float maxFallSpeed = -5;

    [HideInInspector] public float defaultMoveAcceleration;
    [HideInInspector] public float defaultMoveDeceleration;
    [HideInInspector] public float currentXSpeed;
    [HideInInspector] public float currentYSpeed;
    [HideInInspector] public Vector3 deltaPosition;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public Vector3 oldVelocity;
    [HideInInspector] public float initialGravity = 5;

    private int lastDirection = 1;

    public void ApplyFrictionX(float ammount, float airMultiplier)
    {
        float air = 1;
        if (Mathf.Abs(currentXSpeed) <= 0) { return; }
        if ((Mathf.Abs(currentXSpeed) - defaultMoveDeceleration * ammount * air * Time.deltaTime) >= 0)
        {
            if (!IsGrounded()) { air = airMultiplier; }
            currentXSpeed -= defaultMoveDeceleration * ammount * air * GetCurrentDir() * Time.deltaTime;
        }
        else
        {
            currentXSpeed = 0;
        }
    }

    public void ApplyGravity(float multiplier = 1)
    {
        currentYSpeed += initialGravity * multiplier * Time.deltaTime;

        if (currentYSpeed < maxFallSpeed)
        {
            currentYSpeed = maxFallSpeed;
        }
    }

    public Vector3 GetNewDeltaPosition()
    {
        velocity.x = currentXSpeed;
        velocity.y = currentYSpeed;
        oldVelocity = velocity;
        return (oldVelocity + velocity) * 0.5f * Time.deltaTime;
    }

    public int GetCurrentDir()
    {
        int currentDir = lastDirection;
        if (currentXSpeed > 0)
        {
            currentDir = 1;
            lastDirection = 1;
        }
        else if (currentXSpeed < 0)
        {
            currentDir = -1;
            lastDirection = -1;
        }
        return currentDir;
    }

    public bool IsGrounded()
    {
        return mover2D.collisions.below;
    }

    public bool IsBumpingHead()
    {
        return mover2D.collisions.above;
    }

    public virtual void CalculateInitialGravity()
    {
        Debug.Log("Gravity not being calculated in subclass. Setting to default gravity of 5.");
        initialGravity = 5;
    }

    public abstract void ApplyDefaultHorizontalMovement(int dir);
}
