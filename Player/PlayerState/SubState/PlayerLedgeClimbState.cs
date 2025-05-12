using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 corrnerPos;// vị trí góc 
    private Vector2 StartPos;
    private Vector2 stopPos;
    private Vector2 workspace;
    private bool isHanging;
    private bool isClimbing;
    private int xInput;
    private int yInput;
    private bool jumpInput;
    private bool isTouchingCeiling;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    private CollectionSenses collectionSenses;
    private CollectionSenses CollectionSenses
    {
        get => collectionSenses ?? core.GetCoreComponent(ref collectionSenses);
    }
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        player.anim.SetBool("climbLedge", false);

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();
        
        
        Movement.SetVelovityZero();
        player.transform.position = detectedPos;
        corrnerPos = DetermineCornerPosition();
        StartPos.Set(corrnerPos.x - (Movement.facingDirection * playerData.startOffset.x), corrnerPos.y - playerData.startOffset.y);
        stopPos.Set(corrnerPos.x + (Movement.facingDirection * playerData.stopOffset.x), corrnerPos.y + playerData.stopOffset.y);
        player.transform.position = StartPos;
    }

    public override void Exit()
    {
        base.Exit();
        isHanging = false;
        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;

        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
             if (isTouchingCeiling)
            {
                Debug.Log(isTouchingCeiling);
                stateMachine.ChangeState(player.crouchIdleState);
            }
             else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
        else
        {
        xInput = player.inputHandler.NormInputX;
        yInput = player.inputHandler.NormInputY;
        jumpInput = player.inputHandler.JumpInput;

        Movement?.SetVelovityZero();
        player.transform.position = StartPos;
            if (xInput==Movement.facingDirection&&isHanging&& !isClimbing)
        {
            CheckForSpace();
            isClimbing = true;
            player.anim.SetBool("climbLedge", true);
        }
        else if(yInput==-1&&isHanging && !isClimbing)
        {
                Debug.Log("Vào Air 1");
            stateMachine.ChangeState(player.InAirState);
        }
        else if(jumpInput && !isClimbing)
        {
            player.wallJumpState.DetermineWallJumpDirection(true);
            stateMachine.ChangeState(player.wallJumpState);
        }

        }

    }

    public void SetDetectedPos(Vector2 pos) => detectedPos = pos;
    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(corrnerPos + (Vector2.up * 0.015f) + (Vector2.right * Movement.facingDirection * 0.015f), Vector2.up, playerData.standColliderHeight,CollectionSenses.WhatIsGround);
        player.anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }
    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(CollectionSenses.WallCheckPos.position, Vector2.right * Movement.facingDirection, CollectionSenses.WallDistamce, CollectionSenses.WhatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * Movement.facingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(CollectionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down, CollectionSenses.LedgeCheckHorizontal.position.y - CollectionSenses.WallCheckPos.position.y, CollectionSenses.WhatIsGround);
        float yDist = yHit.distance;
        workspace.Set(CollectionSenses.WallCheckPos.position.x + (xDist * Movement.facingDirection), CollectionSenses.LedgeCheckHorizontal.position.y - yDist);
        return workspace;
    }
}
