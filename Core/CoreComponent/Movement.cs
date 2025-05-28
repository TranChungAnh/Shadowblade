using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb { get; private set; }
    public bool canSetVelocity;
    public Vector2 CurrentVelocity { get; private set;}
    private Vector2 workspace;
    public int facingDirection;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponentInParent<Rigidbody2D>();
        facingDirection = 1;
        canSetVelocity = true;

    }

    public override void LogicUpdate()
    {
        CurrentVelocity = rb.velocity;

    }
    public void SetVelovityZero()
    {
        workspace= Vector2.zero;
        SetFinalVelocity();
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        
        workspace = direction * velocity;
        SetFinalVelocity();
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }
    private void SetFinalVelocity()
    {
        if (canSetVelocity)
        {
            rb.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }
    public void CheckIfFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        rb.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
