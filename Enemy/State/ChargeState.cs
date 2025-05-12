using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//trạng thái lao tới (Charge) của kẻ địch 
public class ChargeState : State
{
    protected D_ChargeState chargeData;
    protected bool isPlayerInMinAgroRange;// phạm vi kẻ địch sẽ phát hiện và tấn công người chs 
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollectionSenses collectionSenses;
    private CollectionSenses CollectionSenses
    {
        get => collectionSenses ?? core.GetCoreComponent(ref collectionSenses);
    }
    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState changeData) : base(entity, stateMachine, animBoolName)
    {
        this.chargeData = changeData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = CollectionSenses.CheckIfLedgeVertical();
        
        isDetectingWall = CollectionSenses.CheckIfToucingWall();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        Movement?.SetVelocityX(chargeData.chargeSpeed*Movement.facingDirection);
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(chargeData.chargeSpeed * Movement.facingDirection);
        if (Time.time>= startTime+chargeData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
  
    }
}
