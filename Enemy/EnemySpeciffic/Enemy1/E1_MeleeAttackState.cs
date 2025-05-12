using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttackState : MeleeAttackState
{
    private Enemy1 enemy1;
    public E1_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState meleeAttackData, Enemy1 enemy1) : base(entity, stateMachine, animBoolName, attackPosition, meleeAttackData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinish)
        {
            if (isPlayerInMinArgoRange)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
