using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
  
    private ParticleManager ParticleManager =>
        particManager ? particManager : core.GetCoreComponent(ref particManager);

    private ParticleManager particManager;
    private States Stats => stats ? stats : core.GetCoreComponent(ref stats);
    private States stats;

    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticles(particle);
        }


        GameObject entity = core.transform.parent.gameObject;

    
        entity.SetActive(false);
        SoundManager.Instance.PlaySound(SoundManager.Instance.deathSound);
    }

    private void OnEnable()
    {
        Stats.OnHealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;
    }
}
