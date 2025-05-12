using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState meleeAttackData;
   


    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState meleeAttackData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.meleeAttackData = meleeAttackData;
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

        if (attackPosition == null || meleeAttackData == null)
        {
            Debug.LogError("Attack position or melee attack data is not set.");
            return;
        }

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, meleeAttackData.attackRadius, meleeAttackData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(meleeAttackData.attackDamage);
                if (States.Instance != null)
                {
                    float currentHealth = States.Instance.currentHealth;
                    if (currentHealth > 0)
                    {
                        States.Instance.setHealth(currentHealth - meleeAttackData.attackDamage);
                    }
                }
                else
                {
                    Debug.LogError("States.Instance is null.");
                }
            }

            IKnockBack knockbackable = collider.GetComponent<IKnockBack>();

            if (knockbackable != null)
            {
                knockbackable.KnockBack(meleeAttackData.knockbackAngle, meleeAttackData.knockbackForce, Movement.facingDirection);
            }
        }
    }
}
