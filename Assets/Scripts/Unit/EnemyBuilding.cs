using UnityEngine;

public class EnemyBuilding : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 500;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Check if the building is already destroyed
        if (health <= 0)
        {
            return;
        }

        health -= damage;

        // Check if the building's health is below zero
        if (health <= 0)
        {
            DestroyBuilding();
        }
    }

    void DestroyBuilding()
    {
        Destroy(gameObject);
    }
}
