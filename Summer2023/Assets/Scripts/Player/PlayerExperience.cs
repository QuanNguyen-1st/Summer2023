using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    private int _maxExperience;
    public int CurrentExperience;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _currentLevel;
    private int _diffExperience;
    private void Start() {
        // ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
        _diffExperience = 500;
        _maxExperience = 500;
        _currentLevel = 1;
        CurrentExperience = 0;
    }
    private void Update() {
        _scoreText.text = "Score: " + CurrentExperience.ToString();
    }
    private void OnEnable() {
        StartCoroutine(WaitAdd());
    }
    private IEnumerator WaitAdd() {
        yield return new WaitUntil(()=> ExperienceManager.Instance != null);
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }
    private void OnDisable() {
        StartCoroutine(WaitSub());
    }
    private IEnumerator WaitSub() {
        yield return new WaitUntil(()=> ExperienceManager.Instance != null);
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }
    private void HandleExperienceChange(int newExperience) {
        CurrentExperience += newExperience;
        if (CurrentExperience >= _maxExperience) {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        HealthManager.Instance?.AddHealth(1);
        _currentLevel++;
        // _currentExperience -= _maxExperience;
        _diffExperience += 500;
        _maxExperience = _maxExperience + _diffExperience;
        
    }
}
