using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;
    protected bool flipAfterIdle;
    protected float idleTime;
    protected bool isIdelTimeOver;
    protected bool isPlayerInMinAgroRange;
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(0f);
        isIdelTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
           Movement?.Flip();
        }
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(0f);
        if (Time.time >= startTime + idleTime)
        {
            isIdelTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }
    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
