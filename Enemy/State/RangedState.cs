using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : AttackState
{
    protected D_RangeAttackState rangeData;
    protected GameObject projectTitle;
    protected projectTitle projectTitleScrip;

    public RangedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition,D_RangeAttackState rangeData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.rangeData = rangeData;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        projectTitle = GameObject.Instantiate(rangeData.projectile, attackPosition.position, attackPosition.rotation);
        projectTitleScrip = projectTitle.GetComponent<projectTitle>();
        projectTitleScrip.FireProjectile(rangeData.projecTileSpeed, rangeData.projectTitleDamage, rangeData.projectTitleTraveDistance);


    }
}
