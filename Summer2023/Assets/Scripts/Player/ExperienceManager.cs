using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance {get; private set;}
    public delegate void ExperienceChangeHandler(int amount);
    private void Awake() {
        Instance = this;
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    public event ExperienceChangeHandler OnExperienceChange;
    public void AddExperience(int amount) {
        OnExperienceChange?.Invoke(amount);
    }
}
