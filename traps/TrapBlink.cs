using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlink : trap
{
    private bool isCheckPlayer;
    private bool isCheckWall;
    private bool isCheckGround;


    [Header("Raycast Settings")]
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private Transform checkWall;
    [SerializeField] private Transform checkGround;

    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsPGround;
    [SerializeField] private float distanceWall;
    [SerializeField] private float distanceGround;
    [SerializeField] private float distancePlayer= 6;
    [SerializeField] private float blinkDirection=1;

    [Header("Attack Settings")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private Animator anim;

    private Rigidbody2D rb;
    private bool hasAttacked = false; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        base.Update();
        isCheckPlayer = Physics2D.Raycast(checkPlayer.position, Vector2.right*blinkDirection, distancePlayer, whatIsPlayer);
        isCheckWall = Physics2D.Raycast(checkWall.position, Vector2.right* blinkDirection, distanceWall, whatIsPGround);
        isCheckGround = Physics2D.Raycast(checkGround.position, Vector2.down, distanceGround, whatIsPGround);
        BlinkWall();
    }

    private void BlinkAttackPlayer()
    {
        if (isCheckPlayer && !hasAttacked)
        {
            hasAttacked = true;
            rb.velocity = new Vector2(speed* blinkDirection, rb.velocity.y);
        }

     
    }
    private void BlinkWall()
    {
        if (isCheckWall || !isCheckGround)
        {
            rb.velocity = Vector2.zero;
            hasAttacked = false;
            isCheckWall = false;
            isCheckGround = false;
            blinkDirection = -blinkDirection;

        }
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            death.Die();
            states.Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(checkPlayer.position, checkPlayer.position + Vector3.right * distancePlayer * blinkDirection);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(checkWall.position, checkWall.position + Vector3.right * distanceWall * blinkDirection);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(checkGround.position, checkGround.position + Vector3.down * distanceGround);
    }


}
