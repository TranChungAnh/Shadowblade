using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private float velocityToSet;
    private bool setVelocity;
    private int xInput;
    private bool isCheckFilp;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        isAbilityDone = true;
    }

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySound(SoundManager.Instance.swordSound);
         weapon.EnterWeapon();
        setVelocity = false;
    }
    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this,core);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.inputHandler.NormInputX;
        if (isCheckFilp) 
        { 
        Movement?.CheckIfFlip(xInput);
        }
        if (setVelocity)
        {
            Movement?.SetVelocityX(velocityToSet * Movement.facingDirection);
        }
       
    }
    public void  SetPlayerVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * Movement.facingDirection);
        velocityToSet = velocity;
        setVelocity = true;
    }
    public void SetCheckFlip(bool flip)
    {
        isCheckFilp = flip;
    }
}
