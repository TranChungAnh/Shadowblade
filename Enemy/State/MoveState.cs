using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isPlayerInMinAgroRange;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollectionSenses collectionSenses;
    private CollectionSenses CollectionSenses
    {
        get => collectionSenses ?? core.GetCoreComponent(ref collectionSenses);
    }
    public MoveState(Entity entity, FiniteStateMachine stateMachine,D_MoveState stateData,string animBoolName) : base(entity, stateMachine,animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = CollectionSenses.CheckIfLedgeVertical();
        isDetectingWall = CollectionSenses.CheckIfToucingWall();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(stateData.movementSpeed*Movement.facingDirection);
        SoundManager.Instance.PlaySound(SoundManager.Instance.walkingSound);

       
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(stateData.movementSpeed * Movement.facingDirection);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();



    }

}
