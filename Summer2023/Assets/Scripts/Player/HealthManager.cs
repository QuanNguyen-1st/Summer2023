using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance {get; private set;}
    public delegate void HealthChangeHandler(int amount);
    public static bool IsInvulnerable = false;
    private void Awake() {
        Instance = this;
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    public event HealthChangeHandler OnHealthChange;
    public void AddHealth(int amount) {
        if (amount < 0) {
            if (!IsInvulnerable) { OnHealthChange?.Invoke(amount); }
        }
        else { OnHealthChange?.Invoke(amount); }
        
    }
}
