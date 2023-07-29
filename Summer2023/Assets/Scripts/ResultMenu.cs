using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultMenu : MonoBehaviour
{
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private TMP_Text _winScore;
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private TMP_Text _loseScore;
    [SerializeField] private AudioSource _winAudioSource;
    [SerializeField] private AudioSource _loseAudioSource;
    public static bool IsDone;
    private void Start() {
        IsDone = false;
        _winMenu.SetActive(false);
        _loseMenu.SetActive(false);
    }
    public void GoToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
        // IsDone = false;
    }
    public void WinGame(int score) {
        // DisableNeeds();
        
        IsDone = true;
        Time.timeScale = 0;
        _winMenu.SetActive(true);
        _winAudioSource.Play();
        _winScore.text = "Your score is: " + score;
    }
    public void LoseGame(int score) {
        // Debug.Log(score);
        // DisableNeeds();
        
        IsDone = true;
        Time.timeScale = 0;
        _loseMenu.SetActive(true);
        _loseAudioSource.Play();
        _loseScore.text = "Your score is: " + score;
    }
    public void DisableNeeds() {
        AbilityHolder[] abils = FindObjectsOfType<AbilityHolder>();
        foreach (AbilityHolder abil in abils) {
            abil.enabled = false;
        } 
    }
    
}
