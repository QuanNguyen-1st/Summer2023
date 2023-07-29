using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Summer2023/Enemy Data")]
public class EnemyStatSO : ScriptableObject
{
    // [Header("Health")]
    // public float InitHealth = 5;
    // public bool IsHurtable = true;
    // private float _maxHealth;
    // public float MaxHealth
    // {
    //     get => _maxHealth;
    //     set
    //     {
    //         OnChangeMaxHealth?.Invoke(value, value - _maxHealth);
    //         _maxHealth = value;
    //     }
    // }
        
    // private float _currentHealth;
    // public float CurrentHealth
    // {
    //     get => _currentHealth;
    //     set
    //     {
    //         if (!IsHurtable) return;
            
    //         OnChangeCurrentHealth?.Invoke(value, value - _currentHealth);
    //         _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    //     }
    // }
    // public Action<float, float> OnChangeCurrentHealth;
    // public Action<float, float> OnChangeMaxHealth;
    public String EnemyName;
    [Header("Run")]
    public float RunMaxSpeed; //Target speed we want the player to reach.
    public float RunAcceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
    public float RunDeceleration;
    [Header("Experience")]
    public int Exp;
    [Header("Points")]
    public int NumberOfPoints; //Number of points
    public float PointRadius;
    [Header("Despawn")]
    public float DespawnDistance;
    [Header("Enemy Type")]
    public float AttackDistance;
    public bool IsRange;
    #region IfEnemyIsRange
    [Header("Amount of Bullets")]
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public GameObject BulletPrefab;
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public float BulletMoveSpeed;
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public int BurstCount;
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public int ProjectilePerBurst;
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    [Range(0, 359)] public float AngleSpread;
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public float StartingDistance = 0.1f;
    [Header("Cooldown")]
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public float TimeBetweenBurst;
    public float RestTime = 1f;
    [Header("Shoot Options")]
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public bool Stagger;
    [DrawIf("IsRange", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public bool Oscillate;
    #endregion
    
    #region IfEnemyIsMelee
    [DrawIf("IsRange", false, ComparisonType.Equals, DisablingType.DontDraw)]
    public float ChargeTime;
    #endregion
}
