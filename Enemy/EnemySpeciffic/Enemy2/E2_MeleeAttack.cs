using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MeleeAttack : MeleeAttackState
{
    private Enemy2 enemy2;
    public E2_MeleeAttack(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState meleeAttackData, Enemy2 enemy2) : base(entity, stateMachine, animBoolName, attackPosition, meleeAttackData)
    {
        this.enemy2 = enemy2;
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
                stateMachine.ChangeState(enemy2.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy2.lookForPlayerState);
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
