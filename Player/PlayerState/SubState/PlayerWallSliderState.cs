using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSliderState : PlayerTouchingWallState
{
    public PlayerWallSliderState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Movement?.SetVelocityY(-playerData.wallSlideVelocity);
            if (yInput == 0 && grabInput)
            {
                Debug.Log("isTouchingWall 2222" + isTouchingWall);

                stateMachine.ChangeState(player.wallGrabState);
            }
        }
      
    }
}
