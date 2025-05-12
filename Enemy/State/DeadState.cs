using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState deadData;
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState deadState) : base(entity, stateMachine, animBoolName)
    {
        this.deadData = deadState;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameObject.Instantiate(deadData.deadBloodParticles, entity.transform.position, entity.transform.rotation);
        GameObject.Instantiate(deadData.deadChunkParticles, entity.transform.position, entity.transform.rotation);
        entity.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
