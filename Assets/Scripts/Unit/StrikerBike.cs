using UnityEngine;

public class StrikerBike : BaseUnit
{
    public override float Damage => 5f;
    public override float AttackRange => 10f;
    public override float FireRate => 2f;

    [SerializeField] private float initialHealth = 100f;

    protected override void Start()
    {
        base.Start();
        health = initialHealth;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 0.5f) // Corresponding to 2 attacks per second
        {
            if (target != null && target.health > 0)
            {
                target.TakeDamage(Damage);
            }
            else
            {
                isAttacking = false;
                target = null;
            }
            attackTimer = 0f;
        }
    }
}