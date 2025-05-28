using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSenses : CoreComponent
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
   
    public Transform GroundCheckPos 
    { get => GenericNotImplementError<Transform>.TryGet(groundCheckPos,core.transform.parent.name);
      private set => groundCheckPos = value;
    }
    public Transform WallCheckPos
    {
        get => GenericNotImplementError<Transform>.TryGet(wallCheckPos, core.transform.parent.name);
        private set => wallCheckPos = value;
    }
    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementError<Transform>.TryGet(ledgeCheckHorizontal , core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }
    public Transform CeilingCheckPos
    {
        get => GenericNotImplementError<Transform>.TryGet(ceilingCheckPos, core.transform.parent.name);
        private set => ceilingCheckPos = value;
    }
    public float GroundCheckRadius 
    { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallDistamce 
    { get => wallDistamce; set => wallDistamce = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallDistamce;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField]
    private Transform groundCheckPos;
    [SerializeField]
    private Transform wallCheckPos;
    [SerializeField]
    private Transform ledgeCheckHorizontal;
    [SerializeField]
    private Transform ledgeCheckVertical;
    [SerializeField]
    private Transform ceilingCheckPos;
   
    public bool CheckIfCeiling()
    {
        return Physics2D.OverlapCircle(CeilingCheckPos.position, groundCheckRadius, whatIsGround);
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheckPos.position, groundCheckRadius, whatIsGround);
    }
    public bool CheckIfToucingWall()
    {
        return Physics2D.Raycast(WallCheckPos.position, Vector2.right * Movement.facingDirection, wallDistamce, whatIsGround);
    }
    public bool CheckIfToucingWallBack()
    {
        return Physics2D.Raycast(WallCheckPos.position, Vector2.right * -Movement.facingDirection, wallDistamce, whatIsGround);
    }
    public bool CheckIfLedgeHorizontal()
    {
        return Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.facingDirection, wallDistamce, whatIsGround);
    }
    public bool CheckIfLedgeVertical()
    {
        return Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallDistamce, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        // Dùng giá trị mặc định an toàn
        float facingDirection = 1f;
        if (Application.isPlaying && movement != null)
        {
            facingDirection = movement.facingDirection;
        }

        // Ground check
        Gizmos.color = Color.green;
        if (groundCheckPos != null)
            Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);

        //// Ceiling check
        //Gizmos.color = Color.red;
        //if (ceilingCheckPos != null)
        //    Gizmos.DrawWireSphere(ceilingCheckPos.position, groundCheckRadius);

        // Wall check
        Gizmos.color = Color.blue;
        if (wallCheckPos != null)
        {
            Vector3 direction = Vector3.right * facingDirection * wallDistamce;
            Gizmos.DrawLine(wallCheckPos.position, wallCheckPos.position + direction); // mặt trước
            Gizmos.DrawLine(wallCheckPos.position, wallCheckPos.position - direction); // mặt sau
        }

        //// Ledge check horizontal
        //Gizmos.color = Color.yellow;
        //if (ledgeCheckHorizontal != null)
        //{
        //    Gizmos.DrawLine(ledgeCheckHorizontal.position,
        //        ledgeCheckHorizontal.position + Vector3.right * facingDirection * wallDistamce);
        //}

        // Ledge check vertical
        if (ledgeCheckVertical != null)
        {
            Gizmos.DrawLine(ledgeCheckVertical.position,
                ledgeCheckVertical.position + Vector3.down * wallDistamce);
        }
    }




}
