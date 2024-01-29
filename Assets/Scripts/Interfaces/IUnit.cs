using static UnityEngine.GraphicsBuffer;
using UnityEngine;

public interface IUnit
{
    float Damage { get; }
    float AttackRange { get; }
    float FireRate { get; }

    void SetTarget(EnemyBuilding target);
    void MoveTowardsTarget(EnemyBuilding target);
    void StopAction();
    void ResetState();

}