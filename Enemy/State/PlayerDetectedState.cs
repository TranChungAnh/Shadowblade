using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// enemy phát hiên  ra player 
public class PlayerDetectedState : State
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollectionSenses collectionSenses;
    private CollectionSenses CollectionSenses
    {
        get => collectionSenses ?? core.GetCoreComponent(ref collectionSenses);
    }
    protected D_PlayerDetected detectedData;
    protected bool isPlayerInMinAgrorange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;// có thực hiện hành động tấn công tầm xa
    protected bool performCloseRangeAction;//hành động cận chiến
    protected bool isDetectingLedge;// khi phát hiện player mà ko phát hiện gờ 
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_PlayerDetected detectedData) : base(entity, stateMachine, animBoolName)
    {
        this.detectedData = detectedData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isPlayerInMinAgrorange = entity.CheckPlayerInMinAgroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isDetectingLedge = CollectionSenses.CheckIfLedgeVertical();
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
        Movement?.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(0f);
        if (Time.time>= startTime + detectedData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    
    }
}
