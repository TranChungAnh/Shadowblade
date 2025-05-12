using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DetectedState : PlayerDetectedState
{
    private Enemy1 enemy1;
    public E1_DetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected detectedData,Enemy1 enemy1) : base(entity, stateMachine, animBoolName, detectedData)
    {
        this.enemy1 = enemy1;
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
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy1.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy1.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy1.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            
           Movement?.Flip();
            
            stateMachine.ChangeState(enemy1.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
