using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_weaponData weaponData;
    protected Animator baseAnim;
    protected Animator weaponAnim;
    protected PlayerAttackState attackState;
    protected int attackCount;
    protected Core core;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
   
    protected virtual void Awake()
    {
        baseAnim = transform.Find("Base").GetComponent<Animator>();
        weaponAnim = transform.Find("Weapon").GetComponent<Animator>();
        
        gameObject.SetActive(false);
        attackCount = 0;
    }

    public virtual void EnterWeapon()
    {
        if (attackCount >= weaponData.amountOfAttack)
        {
            attackCount = 0;
        }
        gameObject.SetActive(true);
        baseAnim.SetBool("attack", true);
        weaponAnim.SetBool("attack", true);
        
        baseAnim.SetInteger("attackCounter", attackCount);
        weaponAnim .SetInteger("attackCounter", attackCount);

    }
    public virtual void ExitWeapon()
    {
        baseAnim.SetBool("attack", false);
        weaponAnim.SetBool("attack", false);
        gameObject.SetActive(false);
        attackCount++;
    }

    #region
    public virtual void AnimationFinishedTrigger()
    {
        attackState.AnimationFinishedTrigger();
    }
    public virtual void AnimationStartMovementTrigger()
    {
        attackState.SetPlayerVelocity(weaponData.movementSpeed[attackCount]);
    }
    public virtual void AnimationStopMovementTrigger()
    {
        attackState.SetPlayerVelocity(0f);
    }
    public virtual void AnimationCheckOffFlip()
    {
        attackState.SetCheckFlip(false);
    }
    public virtual void AnimationActionTrigger()
    {

    }
    public virtual void AnimationCheckOnFlip()
    {
        attackState.SetCheckFlip(true);
    }


    #endregion
    public void InitializeWeapon(PlayerAttackState state,Core core)
    {
        this.attackState = state;
        this.core = core;

    }
}
