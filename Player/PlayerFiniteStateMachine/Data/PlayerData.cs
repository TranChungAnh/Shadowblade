using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/ Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move state")]
    public float movementVelocity = 10f;
    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public float amountOfJumps = 1;
    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;// nhảy cao hơn khi giữ nút nhảy
    [Header("Wall Slide state")]
    public float wallSlideVelocity = 3f;
    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;
    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;
    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float holdTimeScale = 0.25f;
    public float maxHoldTime = 1f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float dashEndYMultiplier = 0.2f;
    public float drag = 5f;
    public float distBetweenAfterImages = 0.5f;
    [Header("CrouchState")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHieght = 0.8f;
    public float standColliderHeight = 1.6f;


}
