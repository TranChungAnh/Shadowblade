using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed;
    private float travelDistance;
    private float xStartPos;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;

    private bool isGravityOn;

    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;

        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        if (isGravityOn)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] damageHit = Physics2D.OverlapCircleAll(damagePosition.position, damageRadius, whatIsPlayer);
        if (damageHit.Length > 0)
        {
            foreach (Collider2D collider in damageHit)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.Damage(10f);
                    Debug.Log("Damage dealt to: " + collider.name);
                    if (States.Instance != null)
                    {
                        float currentHealth = States.Instance.currentHealth;
                        if (currentHealth > 0)
                        {
                            Debug.Log("Current Health: 2222222 " + currentHealth);
                            States.Instance.setHealth(currentHealth - 10f);
                        }
                    }
                    else
                    {
                        Debug.LogError("States.Instance is null.");
                    }
                }
            }
        }

        if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
        {
            isGravityOn = true;
            rb.gravityScale = gravity;
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu chạm vào layer "Ground"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Grounded"))
        {
            // Dừng lại hoàn toàn
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.gravityScale = 0f;

            // Nếu muốn xoay về góc mặc định
            transform.rotation = Quaternion.identity;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
