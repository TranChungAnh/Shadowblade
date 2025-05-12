using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState idleState { get; private set; }
    public E2_moveState moveState { get; private set; }
    public E2_playerDetectedState playerDetectedState { get; private set; }
    public E2_MeleeAttack meleeAttackState { get; private set; }
   
    public E2_RangedState rangeState { get; private set; }
    public E2_lookforPlayerState lookForPlayerState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_deadState deadState { get; private set; }
    public E2_dodgeState dodgeState { get; private set; }
    [SerializeField]
    private D_MoveState moveData;
    [SerializeField]
    private D_IdleState idleData;
    [SerializeField]
    private D_PlayerDetected playerDetecteData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackData;
    [SerializeField]
    private D_StunState stunData;
    [SerializeField]
    private D_RangeAttackState rangeData;
    [SerializeField]
    private D_lookForPlayer lookForPlayerData;
    [SerializeField]
    public D_dodgeState dodgeData;
    [SerializeField]
    private D_DeadState deadData;


    [SerializeField]
    private Transform meleeAttackPos;
    [SerializeField]
    private Transform rangAttackPosition;

    public override void Awake()
    {
        base.Awake();
        idleState = new E2_IdleState(this, stateMachine, "idle", idleData, this);
        moveState = new E2_moveState(this, stateMachine, moveData, "move", this);
        playerDetectedState = new E2_playerDetectedState(this, stateMachine, "playerDetected", playerDetecteData, this);
        meleeAttackState = new E2_MeleeAttack(this, stateMachine, "meleeAttack", meleeAttackPos, meleeAttackData, this);
        rangeState = new E2_RangedState(this, stateMachine, "rangeAttack",rangAttackPosition,rangeData,this);
        lookForPlayerState = new E2_lookforPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", stunData, this);
        dodgeState = new E2_dodgeState(this, stateMachine, "dodge", dodgeData, this);
        deadState = new E2_deadState(this, stateMachine, "dead", deadData, this);

    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }
    //public override void Damage1(AttackDetails attackDetails)
    //{
    //    base.Damage1(attackDetails);
    //    if (isDead)
    //    {
    //        stateMachine.ChangeState(deadState);
    //    }
    //    else if (isStunned && stateMachine.currentState != stunState)
    //    {
    //        stateMachine.ChangeState(stunState);
    //    }
    //    else if (!CheckPlayerInMinAgroRange())
    //    {
    //        lookForPlayerState.SetTurnImmediately(true);
    //        stateMachine.ChangeState(lookForPlayerState);
    //    }
    //    else if (CheckPlayerInMinAgroRange())
    //    {
    //        stateMachine.ChangeState(rangeState);
    //    }
    //}

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPos.position, meleeAttackData.attackRadius);
    }
}
