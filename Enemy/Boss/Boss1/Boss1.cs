using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Entity
{
    public B1_idleState idleState { get; private set; }
    public B1_moveState moveState { get; private set; }
    public B1_playerDetectedState playerDetectedState { get; private set; }
    public B1_meleeAttackState meleeAttackState { get; private set; }
    public B1_rangeState rangeState { get; private set; }
    public B1_lookForPlayer lookForPlayerState { get; private set; }
    public B1_stunState stunState { get; private set; }
    public B1_deathState deadState { get; private set; }
    public B1_jumpState jumpState { get; private set; }

    [SerializeField] private D_MoveState moveData;
    [SerializeField] private D_IdleState idleData;
    [SerializeField] private D_PlayerDetected playerDetecteData;
    [SerializeField] private D_MeleeAttackState meleeAttackData;
    [SerializeField] private D_StunState stunData;
    [SerializeField] private D_RangeAttackState rangeData;
    [SerializeField] private D_lookForPlayer lookForPlayerData;
    [SerializeField] public D_dodgeState dodgeData;
    [SerializeField] private D_DeadState deadData;

    [SerializeField] private Transform meleeAttackPos;
    [SerializeField] private Transform rangAttackPosition;

    //// --- BẮN ĐẠN THEO HƯỚNG ---
    //[SerializeField] private GameObject bulletPrefab; // Prefab của quả cầu
    //[SerializeField] private Transform firePoint;     // Vị trí spawn quả cầu
    //[SerializeField] private float fireInterval = 10f; // Khoảng thời gian giữa mỗi đợt bắn
    //[SerializeField] private int numberOfProjectiles = 8; // Số quả cầu bắn ra mỗi lần
    //[SerializeField] private float projectileSpeed = 5f; // Tốc độ quả cầu

    public override void Awake()
    {
        base.Awake();
        idleState = new B1_idleState(this, stateMachine, "idle", idleData, this);
        moveState = new B1_moveState(this, stateMachine, moveData, "move", this);
        playerDetectedState = new B1_playerDetectedState(this, stateMachine, "playerDetected", playerDetecteData, this);
        meleeAttackState = new B1_meleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPos, meleeAttackData, this);
        rangeState = new B1_rangeState(this, stateMachine, "rangeAttack", rangAttackPosition, rangeData, this);
        lookForPlayerState = new B1_lookForPlayer(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        stunState = new B1_stunState(this, stateMachine, "stun", stunData, this);
        jumpState = new B1_jumpState(this, stateMachine, "jump", dodgeData, this);
        deadState = new B1_deathState(this, stateMachine, "dead", deadData, this);
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
        //StartCoroutine(FireProjectilesRoutine()); // Bắt đầu coroutine bắn đạn mỗi 10s
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

        // Vẽ phạm vi tấn công cận chiến
        if (meleeAttackPos != null)
            Gizmos.DrawWireSphere(meleeAttackPos.position, meleeAttackData.attackRadius);

        // Vẽ phạm vi phát hiện người chơi
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, entityData.minAgroDistance);

        // Vẽ phạm vi tấn công tầm xa
        if (rangAttackPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(rangAttackPosition.position, entityData.maxAgroDistance);
        }

        // Reset lại màu (tùy chọn)
        Gizmos.color = Color.white;
    }


    // ---------------------------
    // BẮN CÁC QUẢ CẦU NHIỀU HƯỚNG
    // ---------------------------
    //private IEnumerator FireProjectilesRoutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(fireInterval);
    //        FireProjectilesInAllDirections();
    //    }
    //}

    //private void FireProjectilesInAllDirections()
    //{
    //    float angleStep = 360f / numberOfProjectiles;
    //    float angle = 0f;

    //    for (int i = 0; i < numberOfProjectiles; i++)
    //    {
    //        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
    //        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
    //        Vector2 dir = new Vector2(dirX, dirY).normalized;

    //        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    //        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    //        if (rb != null)
    //        {
    //            rb.velocity = dir * projectileSpeed;
    //        }

    //        angle += angleStep;
    //    }
    //}
}
