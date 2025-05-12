using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerInputHander;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerInputHander inputHandler { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerlandState landState { get; private set; }
    public PlayerWallClimbState wallClimbState { get; private set; }
    public PlayerWallSliderState wallSlideState { get; private set; }
    public PlayerWallGrabState wallGrabState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerLedgeClimbState ledgeClimbState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerCrouchIdle crouchIdleState { get; private set; }
    public PlayerCrouchMove crouchMoveState { get; private set; }
    public PlayerAttackState primaryAttackState { get; private set; }
    public PlayerAttackState secondaryAttackState { get; private set; }
    public PlayerInventory inventory { get; private set; }
    public Core core { get; private set; }

    public Animator anim { get; private set; }
    public BoxCollider2D movementCollider { get; private set; }
   
    public Transform dashDirectionIndicator;
    public Rigidbody2D rb { get; private set; }
    private Vector2 workspace;

    [SerializeField]
    private PlayerData playerData;
    public float gameStartTime;
    private void Awake()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.BGSound);
        core = GetComponentInChildren<Core>();

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
        landState=new PlayerlandState(this, stateMachine, playerData, "land");
        wallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");
        wallSlideState= new PlayerWallSliderState(this, stateMachine, playerData, "wallSlide");
        wallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
        ledgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimbState");
        dashState=new PlayerDashState(this, stateMachine, playerData, "inAir");
        crouchMoveState = new PlayerCrouchMove(this, stateMachine, playerData, "crouchMove");
        crouchIdleState = new PlayerCrouchIdle(this, stateMachine, playerData, "crouchIdle");
        primaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
        secondaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
        gameStartTime = Time.time;
    }
    private void Start()
    {

        anim=GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHander>();
        rb=GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
        dashDirectionIndicator = transform.Find("DashDirectionIndicator");
        movementCollider= GetComponent<BoxCollider2D>();
        inventory = GetComponent<PlayerInventory>();

        primaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
        //secondaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.secondary]);
    }
    private void Update()
    {
        core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }


    public void SetColliderHeight(float height)
    {
        Vector2 center = movementCollider.offset;
        workspace.Set(movementCollider.size.x, height);
        center.y += (height - movementCollider.size.y) / 2;
        movementCollider.size = workspace;
        movementCollider.offset = center;
    }

  
   
  
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishedTrigger();
  
   
}
