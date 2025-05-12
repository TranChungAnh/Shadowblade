using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected int xInput;
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;
    protected int yInput;
    protected bool grabInput;
    protected bool jumpInput;
    private float wallContactBufferTime = 0.1f;
    private float lastWallTouchTime;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollectionSenses collectionSenses;
    private CollectionSenses CollectionSenses
    {
        get => collectionSenses ?? core.GetCoreComponent(ref collectionSenses);
    }
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }



    public override void Dochecks()
    {
        base.Dochecks();
        if (CollectionSenses)
        {
            isGrounded = CollectionSenses.CheckIfGrounded();
            isTouchingWall = CollectionSenses.CheckIfToucingWall();
            isTouchingLedge = CollectionSenses.CheckIfLedgeHorizontal();

            if (isTouchingWall)
            {
                lastWallTouchTime = Time.time;
            }

            if (isTouchingWall && !isTouchingLedge)
            {
                player.ledgeClimbState.SetDetectedPos(player.transform.position);
            }
        }
    }

    private bool IsTouchingWallBuffered()
    {
        return Time.time < lastWallTouchTime + wallContactBufferTime;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.inputHandler.NormInputX;
        yInput = player.inputHandler.NormInputY;
        grabInput = player.inputHandler.grabInput;
        jumpInput = player.inputHandler.JumpInput;
        if (jumpInput)
        {
            player.inputHandler.UseJumpInput();
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (!IsTouchingWallBuffered() || (!grabInput && xInput != Movement.facingDirection && Mathf.Abs(xInput) > 0.1f))
        {
            stateMachine.ChangeState(player.InAirState);
        }

        else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
