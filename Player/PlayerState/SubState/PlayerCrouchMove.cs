using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCrouchMove : PlayerGroundedState
{
    public PlayerCrouchMove(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetColliderHeight(playerData.crouchColliderHieght);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Movement?.SetVelocityX(playerData.crouchMovementVelocity * Movement.facingDirection);

            Movement?.CheckIfFlip(xInput);

            if (xInput == 0)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
            else if (yInput != -1 && !isCeilingCheck)
            {
                stateMachine.ChangeState(player.moveState);
            }
        }

    }
}