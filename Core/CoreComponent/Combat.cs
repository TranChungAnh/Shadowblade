using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable,IKnockBack
{
    [SerializeField] private GameObject damageParticles;

    public float knockBackStartTime;
    private bool isKnockBackActive;
    [SerializeField] float knockBackMaxTime =0.2f;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    private CollectionSenses CollectionSenses {  get => collectionSenses ?? core.GetCoreComponent(ref collectionSenses);}
    private States States{get => states ?? core.GetCoreComponent(ref states);}

    private Movement movement;
    private ParticleManager particleManager;
    private CollectionSenses collectionSenses;
    private States states;
  
    public override void LogicUpdate()
    {
        CheckKnockBack();
    }
    public void Damage(float amount)
    {
        States.DecreaseHealth(amount);
        ParticleManager?.StartPaticlesWithRandomRotation(damageParticles);
    }

    public void KnockBack(Vector2 angle, float knockbackForce, int direction)
    {
        Movement?.SetVelocity(knockbackForce, angle, direction);
        Movement.canSetVelocity = false;
        isKnockBackActive = true;
        knockBackStartTime = Time.time;
    }
    private void CheckKnockBack()
    {
        if(isKnockBackActive && (Movement?.CurrentVelocity.y<=0.01f&& CollectionSenses.GroundCheckPos) ||Time.time>= knockBackMaxTime+knockBackStartTime )
        {
            isKnockBackActive = false;
            Movement.canSetVelocity = true;
        }
    }

 
}
