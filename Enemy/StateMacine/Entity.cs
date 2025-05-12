using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ham chiếu đến thực thể (nhân vật, kẻ địch, vật thể...) đang sử dụng FSM.
public class Entity : MonoBehaviour
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollectionSenses collectionSenses;
    private CollectionSenses CollectionSenses
    {
        get => collectionSenses ?? core.GetCoreComponent(ref collectionSenses);
    }
    public Animator anim { get; private set; }

    public FiniteStateMachine stateMachine;
    private Vector2 velocityWorkspace;

    public D_Entity entityData;
    [SerializeField]
    private Transform wallCheck, ledgeCheck,groundCheck;
    [SerializeField]
    private Transform playerCheck;
    public AnimationToStateMachine atsm { get; private set; }

    private float currentHealth;
    public int lastDamageDirection { get; private set; }
    public Core core { get; private set; }

    private float currentStunResistance;
    private float lastDamageTime;
    public  bool isStunned;
    public bool isDead;
    public virtual void Awake()
    {
        core = GetComponentInChildren<Core>(); 
        if (core == null)
        {
        }

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        anim = GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();
        atsm = GetComponent<AnimationToStateMachine>();
    }
    public virtual void Update()
    {
        core.LogicUpdate();
        anim.SetFloat("yVelocity",Movement.rb.velocity.y);
        stateMachine.currentState.LogicUpdate();
        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
 
   
    // 2 hàm kiếm tra khoảng cách enemy phát hiện ra player 
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position,transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    // phạm vi tầm gần 
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position,transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
  
    // làm cho enemy nhay lên khi bị tấn công 
    public virtual void DamageHop(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Movement.rb.velocity = velocityWorkspace;
    }
    public void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }
    //public virtual void Damage1(AttackDetails attackDetails)
    //{
        
    //    lastDamageTime = Time.time;
    //    currentHealth -= attackDetails.damageAmount;
    //    currentStunResistance -= attackDetails.stunDamageAmount;
    //    Vector2 jumpAngle = new Vector2(1, 1); // Góc nhảy (1, 1) là góc 45 độ
    //    int direction = attackDetails.position.x > transform.position.x ? -1 : 1; // Hướng nhảy dựa trên vị trí tấn công
    //    DamageHop(entityData.damageHopSpeed, jumpAngle, direction);

    //    Instantiate(entityData.hitParticles, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    //    if (attackDetails.position.x > transform.position.x)
    //    {
    //        lastDamageDirection = -1;
    //    }
    //    else
    //    {
    //        lastDamageDirection = 1;
    //    }
    //    if (currentStunResistance <= 0)
    //    {
    //        isStunned = true;
    //    }
    //    if (currentHealth<=0)
    //    {
    //        isDead = true;
    //    }
    //}
    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();// chẩn hoán vè khoảng (-1,1)
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Movement.rb.velocity = velocityWorkspace;
    }
    public virtual void OnDrawGizmos()
    {
        if(core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.facingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        }


    }
}
