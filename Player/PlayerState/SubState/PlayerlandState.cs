using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerlandState : PlayerGroundedState
{
    public PlayerlandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!isExitingState)
        {

            if (xInput != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }

            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
       
    }
}
