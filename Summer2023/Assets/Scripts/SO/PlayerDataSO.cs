using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Summer2023/Player Data")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerDataSO : ScriptableObject
{
    [Header("Health")]
    public int InitHealth = 5;
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

    [Header("Invincibility")] 
    public float InvincibilityTime = 2f;
    
    [Header("Run")]
    public float RunMaxSpeed; //Target speed we want the player to reach.
    public float RunAcceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
    // [HideInInspector] public float RunAccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
    public float RunDeceleration; //Time (approx.) we want it to take for the player to accelerate from runMaxSpeed to 0.
    // [HideInInspector] public float RunDecelerateAmount; //Actual force (multiplied with speedDiff) applied to the player .
    // [Space(10)]
    // [Range(0.01f, 1)] public float AccelInAir; //Multipliers applied to acceleration rate when airborne.
    // [Range(0.01f, 1)] public float DecelerateInAir;
    // public bool DoConserveMomentum;

    // private void OnValidate()
    // {
    //     //Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
    //     RunAccelAmount = (50 * RunAcceleration) / RunMaxSpeed;
    //     RunDecelerateAmount = (50 * RunDeceleration) / RunMaxSpeed;

    //     #region Variable Ranges
    //     RunAcceleration = Mathf.Clamp(RunAcceleration, 0.01f, RunMaxSpeed);
    //     RunDeceleration = Mathf.Clamp(RunDeceleration, 0.01f, RunMaxSpeed);
    //     #endregion
    // }
}