using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.inputHandler.UseJumpInput();
        player.jumpState.ResetAmountOfJumpLeft();

        Movement?.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        Movement?.CheckIfFlip(wallJumpDirection);
        player.jumpState.DecreaseAmountOfJumpLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.anim.SetFloat("yVelocity",  Movement.CurrentVelocity.y);
        player.anim.SetFloat("xVelocity", Mathf.Abs( Movement.CurrentVelocity.x));
        if(Time.time>= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -Movement.facingDirection;
        }
        else
        {
            wallJumpDirection = Movement.facingDirection;
        }
    }
}
