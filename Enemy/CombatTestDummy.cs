using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour,IDamageable
{
    [SerializeField] private GameObject hitParticles;
    private Animator Anim;
    private  float currentHealth;
    private void Awake()
    {
        currentHealth = 30f;
    }

    public void Damage(float amount)
    {
       Debug.Log("Damage: " + amount);
       Instantiate(hitParticles, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        Anim.SetTrigger("damage");
        currentHealth -= 10;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);

        }
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
}
