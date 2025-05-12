using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdGrabPos;
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void Dochecks()
    {
        base.Dochecks();
    }

    public override void Enter()
    {
        base.Enter();
        holdGrabPos = player.transform.position;
        HoldGrap();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            HoldGrap();
            if (yInput > 0)
            {
                Debug.Log("Wall Climb");
                stateMachine.ChangeState(player.wallClimbState);
            }
            else if (!grabInput || yInput < 0)
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
      
       
    }
    public void HoldGrap()
    {
        player.transform.position = holdGrabPos;

        Movement?.SetVelocityX(0f);
        Movement?.SetVelocityY(0f);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
