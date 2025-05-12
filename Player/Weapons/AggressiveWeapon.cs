using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;
    private List<IDamageable> detectedDamageables = new List<IDamageable>();
    private List<IKnockBack> detectedKnockBackables = new List<IKnockBack>();

    protected override void Awake()
    {
        base.Awake();
        if (weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
        }
        else
        {
        }
    }


    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }
    private void CheckMeleeAttack()
    {
        WeaponAttackDetails attackDetails = aggressiveWeaponData.AttackDetails[attackCount];
        

        foreach (IDamageable item in detectedDamageables.ToList())
        {
            item.Damage(attackDetails.damageAmount);
        }
        foreach(IKnockBack item in detectedKnockBackables.ToList())
        {
            item.KnockBack(attackDetails.knockbackAngle, attackDetails.knockbackForce, Movement.facingDirection);
        }
    }
    public void AddToDetected(Collider2D collision)
    {
        Debug.Log("AddTodetected");
        IDamageable damageable = collision.GetComponent<IDamageable>();
        IKnockBack knockbackable=collision.GetComponent<IKnockBack>();
        if (damageable != null)
        {
            Debug.Log("Add knock back");
            detectedDamageables.Add(damageable);
        }
        if (knockbackable != null)
        {
            detectedKnockBackables.Add(knockbackable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        Debug.Log("RemoveTodetected");
        IDamageable damageable = collision.GetComponent<IDamageable>();
        IKnockBack knockbackable = collision.GetComponent<IKnockBack>();
        if (damageable != null)
        {
            Debug.Log("Remove");

            detectedDamageables.Remove(damageable);
        }
        if (knockbackable != null)
        {
            Debug.Log("Remove knock back");

            detectedKnockBackables.Remove(knockbackable);
        }
    }
}