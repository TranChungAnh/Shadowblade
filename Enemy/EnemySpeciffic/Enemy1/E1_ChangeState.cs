using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChangeState : ChargeState
{
    public Enemy1 enemy1;
    public E1_ChangeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState changeData, Enemy1 enemy1) : base(entity, stateMachine, animBoolName, changeData)
    {
        this.enemy1 = enemy1;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        // tấn công tầm gần 
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy1.meleeAttackState);
        }
        if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy1.lookForPlayerState);
        }
        if (isChargeTimeOver)
        {
           
             if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy1.playerDetectedState);
            }
             else
            {
                stateMachine.ChangeState(enemy1.lookForPlayerState);

            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
