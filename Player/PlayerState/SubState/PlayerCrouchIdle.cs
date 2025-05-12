using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCrouchIdle : PlayerGroundedState
{
    public PlayerCrouchIdle(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

       Movement?.SetVelovityZero();
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
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.crouchMoveState);
            }
            else if (yInput != -1 && !isCeilingCheck)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }
}