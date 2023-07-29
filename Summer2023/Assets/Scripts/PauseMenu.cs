using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    public static bool IsPaused;
    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ResultMenu.IsDone) {
            if (IsPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        // DisableNeeds();
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void ResumeGame() {
        // EnableNeeds();
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void GoToMainMenu() {
        IsPaused = false;
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();
    }
    public void DisableNeeds() {
        AbilityHolder[] abils = FindObjectsOfType<AbilityHolder>();
        foreach (AbilityHolder abil in abils) {
            abil.enabled = false;
        } 
    }

    public void EnableNeeds() {
        AbilityHolder[] abils = FindObjectsOfType<AbilityHolder>();
        foreach (AbilityHolder abil in abils) {
            abil.enabled = true;
        } 
    }
}
