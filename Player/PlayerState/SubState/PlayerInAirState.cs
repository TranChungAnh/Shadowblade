using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerInputHander;

public class PlayerInAirState : PlayerState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollectionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollectionSenses collisionSenses;

    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool dashInput;

    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;

    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Dochecks()
    {
        base.Dochecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.CheckIfGrounded();
            isTouchingWall = CollisionSenses.CheckIfToucingWall();
            isTouchingWallBack = CollisionSenses.CheckIfToucingWallBack();
            isTouchingLedge = CollisionSenses.CheckIfLedgeHorizontal();
        }

        if (isTouchingWall && !isTouchingLedge)
        {
            player.ledgeClimbState.SetDetectedPos(player.transform.position);
        }

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.JumpInput;
        jumpInputStop = player.inputHandler.jumpInputStop;
        grabInput = player.inputHandler.grabInput;
        dashInput = player.inputHandler.dashInput;

        CheckJumpMultiplier();

        if (player.inputHandler.attackInput[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.attackInput[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
        {
            Debug.Log("isTouchingWall 3333" + isTouchingWall);

            stateMachine.ChangeState(player.landState);
        }
        else if (isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = CollisionSenses.CheckIfToucingWall();
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            Debug.Log("Wall Grab");
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (isTouchingWall && xInput == Movement?.facingDirection && Movement?.CurrentVelocity.y <= 0)
        {
            Debug.Log("isTouchingWall 4444" + isTouchingWall);

            stateMachine.ChangeState(player.wallSlideState);
        }
        else if (dashInput && player.dashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else
        {
            Movement?.CheckIfFlip(xInput);
            Movement?.SetVelocityX(playerData.movementVelocity * xInput);

            player.anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
        }

    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > 
            + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.jumpState.DecreaseAmountOfJumpLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    public void SetIsJumping() => isJumping = true;
}
