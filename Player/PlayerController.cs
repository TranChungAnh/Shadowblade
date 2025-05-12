using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.XR;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    private float movementIputDicraction;
    private bool isFacingRight = true;

    public float movementSpeed = 10.5f;
    public float jumpFore = 16.5f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlidingSpeed;
    public float movementForeceInAir;
    public float airDragMulitiplier;
    public float variableJumpHeightMulitiplier;
    public float wallJumpForce;
    public float jumpTimer;
    public float jumpTimerSet = 0.15f;
    public float turnTimer;
    public float turnTimerSet;
    public float wallJumpTimer;
    public float wallJumpTimerSet = 0.5f;

    public float LedgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;

    public float dashSpeed;
    public float dashTime;
    public float distanceBetweenImages;
    public float dashCoolDown;
    public float dashTimeLeft;
    public float lastImageXpos;
    public float lastDash = -100f;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    public Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;

    private bool isWallking;
    private bool isGrounded;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isAttemptingTojump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool isTouchingLedge;
    private bool canClimbLedge;
    private bool ledgedeleted;
    public bool isDashing;

    private Animator anim;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;
    public LayerMask WhatIsGround;

    private int amountOfJumpLeft;
    public int amountOfJump = 1;
    public int facingDirection = 1;
    public int lastWalljumpDirection;

    private float KnockBackStartTime;
    [SerializeField]
    private float knockbackDuration;
    private bool knockBack;
    [SerializeField]
    private Vector2 knockBackSpeed;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpLeft = amountOfJump;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimation();
        CheckIfCanJump();
        CheckIfWallSliding();
        Checkjump();
        CheckLedgeClimb();
        CheckDash();
        CheckKnockBack();
    }
    //thiết lập trạng thái trượt tường
    private void CheckIfWallSliding()
    {
        if (isTouchingWall && movementIputDicraction==facingDirection && rb.velocity.y<0 && !canClimbLedge)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }


    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y<=0.01f) 
        {
            amountOfJumpLeft = amountOfJump;
        }
        if (isTouchingWall)
        {
            canWallJump = true;
        }
         if (amountOfJumpLeft > 0)
        {
            canNormalJump = true;
        }
        else
        {
            canNormalJump = false;
        }
    }


    // đựơc gọi cố định không phụ thuộc vào fbs của máy 
    private void FixedUpdate()
    {
        ApplyMove();
        CheckSurroundings();
    }

    // thực hiện trạng tháy leo qua  tường 
    private void CheckLedgeClimb()
    {
        if (ledgedeleted && !canClimbLedge)
        {
            canClimbLedge = true;
            // khi nhân vật đâng đối mặt sang phải 
            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - LedgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x - wallCheckDistance) + LedgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            canMove = false;
            canFlip = false;

            anim.SetBool("canClimbLedge", canClimbLedge);
        }

      /*  if (canClimbLedge)
        {
            transform.position = ledgePos1; // Di chuyển đến vị trí trung gian
        }*/
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgedeleted = false;
        anim.SetBool("canClimbLedge", canClimbLedge);

    }

    private void CheckMovementDirection()
    {
        if(isFacingRight&& movementIputDicraction < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementIputDicraction>0)
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x)>=0.01f)
        {
            isWallking = true;
        }
        else
        {
            isWallking = false;
        }
    }
    public void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right,wallCheckDistance,WhatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, WhatIsGround);
        if(isTouchingWall && !isTouchingLedge && !ledgedeleted)
        {
            ledgedeleted = true;
            ledgePosBot = wallCheck.position;
        }
    }
    public void DisableFlip()
    {
        canFlip = false;
    }
    public void EnableFlip()
    {
        canFlip = true;
    }
    // lật 
    private void Flip()
    {
        if (canFlip &&!knockBack)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
       
    }
    public void UpdateAnimation()
    {
        anim.SetBool("isWallking", isWallking);
        anim.SetBool("isGround", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }
 
    private void CheckInput()
    {
        movementIputDicraction = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded ||(isTouchingWall && amountOfJumpLeft>0))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingTojump = true;
            }
      
        }
        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if(!isGrounded && movementIputDicraction != facingDirection)
            {
                canMove = false;
                canFlip = false;
                turnTimer = turnTimerSet;
            }
        }
        if (turnTimer>=0)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer < -0)
            {
                canMove = true;
                canFlip = true;
            }
        }
        // nhảy độ cao dựa trên thời gian giữ phím 
        if (checkJumpMultiplier && Input.GetButtonUp("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*variableJumpHeightMulitiplier);
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown)) { 
                AttemptToDash();
            }
       }
    }
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;// Cài đặt thời gian dash còn lại.
        lastDash = Time.time;// Ghi lại thời điểm thực hiện dash.

        PlayerAfterImagePool.Instance.GetFromPool();// Lấy một hình ảnh bóng mờ từ pool.
        lastImageXpos = transform.position.x;
    }
    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
                dashTimeLeft -= Time.deltaTime;
                //Nếu khoảng cách giữa vị trí hiện tại và vị trí của hình ảnh cuối lớn hơn distanceBetweenImages, tạo bóng mờ mới.
                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
            // kết thúc dash 
            if(dashTimeLeft<=0|| isTouchingWall)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }
           
        }
    }
    private void Checkjump()
    {
        if (jumpTimer > 0)
        {
           //wall jump
            if(!isGrounded &&isTouchingWall && movementIputDicraction!=0 && movementIputDicraction != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }
        if(isAttemptingTojump)
        {
            jumpTimer-=Time.deltaTime;
        }
        if (wallJumpTimer > 0)
        {
            if(hasWallJumped && movementIputDicraction == -lastWalljumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);// Hủy vận tốc dọc khi đổi hướng di chuyển ngược lại
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }
    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpFore);
            amountOfJumpLeft--;
            jumpTimer = 0;
            isAttemptingTojump = false;
            checkJumpMultiplier = true ;
        }
    }
    private void WallJump()
    {
        if (canWallJump) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpLeft = amountOfJump;
            amountOfJumpLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpDirection.x * wallJumpForce * movementIputDicraction, wallJumpDirection.y * wallJumpForce);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingTojump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWalljumpDirection = -facingDirection;

        }
    }

    public void ApplyMove()
    {
        if (!isGrounded && !isWallSliding && movementIputDicraction == 0 && !knockBack)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMulitiplier, rb.velocity.y);
        }
        else if (canMove && !knockBack)
        {
            rb.velocity = new Vector2(movementIputDicraction * movementSpeed, rb.velocity.y);
        }
      /*  // ktra xe có nhấn phím sang trái phải ko  
        else if(!isGrounded && !isWallSliding && movementIputDicraction != 0)
        {
            // tạo lực ngang với moveInpu là 1 hoặc -1 
            Vector2 foreToAdd = new Vector2(movementIputDicraction * movementForeceInAir, 0);
            rb.AddForce(foreToAdd);
            // vận tốc ngang tối đa 
            if (Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * movementIputDicraction, rb.velocity.y);
            }
        }*/
       
        if (isWallSliding)
        {
            // nếu tốc độ rơi lớn hơn tốc độ trươt thì hạn chế tốc độ rơi 
            if (rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
        
      
    }


    public void KnockBack(int diraction)
    {
        knockBack = true;
        KnockBackStartTime = Time.time;
        rb.velocity = new Vector2(knockBackSpeed.x * diraction, knockBackSpeed.y);
    }
    private void CheckKnockBack()
    {
        if(Time.time>=knockbackDuration +KnockBackStartTime && knockBack)
        {
            knockBack = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    public bool GetDashStatus()
    {
        return isDashing;
    }












    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
    public int GetFacingDirection()
    {
        return facingDirection;
    }
}
