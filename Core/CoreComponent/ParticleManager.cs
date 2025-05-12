using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : CoreComponent
{
    private Transform particlesContainer;
    protected override void Awake()
    {
        base.Awake();
        particlesContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;

    }
    // tao hieu ung o vt cu the  va xoay cu the 
    public GameObject StartParticles(GameObject particlePrefab,Vector2 position,Quaternion rotation)
    {
        return Instantiate(particlePrefab, position, rotation, particlesContainer);
    }
    // tao hieu ung ngay tai cho object va khong xoay
    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }
    // tao hieu ung ngay tai cho object va xoay ngau nhien
    public GameObject StartPaticlesWithRandomRotation(GameObject particlePrefab)
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }
}
