using UnityEngine;

public class HeavyTank : BaseUnit
{
    public override float Damage => 20f;
    public override float AttackRange => 30f;
    public override float FireRate => 1.5f;

    [SerializeField] private float initialHealth = 900f;

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
        if (attackTimer >= 1.5f) // Corresponding to 0.666 attacks per second
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