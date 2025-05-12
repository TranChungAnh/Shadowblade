using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public float amountOfJumpLeft;
 
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        //SoundManager.Instance.PlaySound(SoundManager.Instance.jumpSound);
        player.inputHandler.UseJumpInput();
        isAbilityDone = true;
        Movement?.SetVelocityY(playerData.jumpVelocity);
        amountOfJumpLeft--;
        player.InAirState.SetIsJumping();
    }
    public bool CanJump()
    {
        if(amountOfJumpLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetAmountOfJumpLeft() => amountOfJumpLeft = playerData.amountOfJumps;
    public void DecreaseAmountOfJumpLeft() => amountOfJumpLeft--;
}
