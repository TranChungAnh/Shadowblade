using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBall : trap
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float distance=0.5f ; 

    private void Update()
    {
        base.Update();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);
        if (hit.collider != null)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            states.Die();
            death.Die();
        }
    }
}
