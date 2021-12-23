using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scoreAmount = 10;
    [SerializeField] float maximumHealth = 100.0f;
    float currentHealth = 0.0f;

    Scoreboard scoreboard;

    private void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        currentHealth = maximumHealth;
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
        if(currentHealth <= Mathf.Epsilon)
        {
            DestroyEnemy();
        }
    }

    private void ProcessHit(GameObject other)
    {
        InstantiateVFX(hitVFX);
        LaserBeam beam = other.GetComponent<LaserBeam>();
        currentHealth -= beam.GetHitDamage();
        scoreboard.UpdateScore(scoreAmount);
    }

    private void DestroyEnemy()
    {
        InstantiateVFX(deathVFX);
        Destroy(gameObject);
    }

    void InstantiateVFX(GameObject chosenVFX)
    {
        GameObject vfx = Instantiate(chosenVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
    }
}
