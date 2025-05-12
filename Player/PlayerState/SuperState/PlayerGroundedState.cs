using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInputHander;
using System.Linq;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    protected bool JumpInput;
    protected bool isCeilingCheck;
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabinput;
    protected bool dashInput;
    private bool isTouchingLedge;

    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    private CollectionSenses CollisionSenses
    {
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }

    private CollectionSenses collisionSenses;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Dochecks()
    {
        base.Dochecks();
        if (CollisionSenses)
        {
        isGrounded = CollisionSenses.CheckIfGrounded();
        isTouchingWall = CollisionSenses.CheckIfToucingWall();
        isCeilingCheck = CollisionSenses.CheckIfCeiling();
        isTouchingLedge = CollisionSenses.CheckIfLedgeHorizontal();
        }
        else
        {
            Debug.LogError("CollisionSenses is null");
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.jumpState.ResetAmountOfJumpLeft();
        player.dashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput =player.inputHandler.NormInputX;
        yInput = player.inputHandler.NormInputY;
        JumpInput = player.inputHandler.JumpInput;
        grabinput = player.inputHandler.grabInput;
        dashInput=player.inputHandler.dashInput;

        if (player.inputHandler.attackInput[(int)CombatInputs.primary] && !isCeilingCheck)
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.attackInput[(int)CombatInputs.secondary] && !isCeilingCheck)
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if (JumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded)
        {

            Debug.Log("air "+isGrounded);
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && grabinput && isTouchingLedge)
        {
            Debug.Log("Wall Grab 1");
            stateMachine.ChangeState(player.wallGrabState );
        }
        else if(dashInput && player.dashState.CheckIfCanDash() && !isCeilingCheck)
        { 
            stateMachine.ChangeState(player.dashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
