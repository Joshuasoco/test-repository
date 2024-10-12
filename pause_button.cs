using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pause_button : MonoBehaviour
{
    public GameObject Pause_button;
    public GameObject Restart_button;
    public GameObject Pause_frame;
    public GameObject Setting_button;
    public GameObject Resume_button;
    public GameObject Mmenu_button;
    public GameObject respawn; // Ensure this is assigned in the Inspector
    private GameObject player;
    private bool isPaused = false; // The game is not paused at the start

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Pause_button.SetActive(true);
        HidePauseMenu();
        
        if (Restart_button != null)
        {
            Button restartbtn = Restart_button.GetComponent<Button>();
            if (restartbtn != null)
            {
                restartbtn.onClick.AddListener(OnRestartButton);
            }
        }
    }

    // Show the pause menu
    public void ShowPause()
    {
        Restart_button.SetActive(true);
        Pause_frame.SetActive(true);
        Setting_button.SetActive(true);
        Resume_button.SetActive(true);
        Mmenu_button.SetActive(true);

        Time.timeScale = 0f; 
        isPaused = true;
    }

    // Hide the pause menu
    public void HidePauseMenu()
    {
        Restart_button.SetActive(false);
        Pause_frame.SetActive(false);
        Setting_button.SetActive(false);
        Resume_button.SetActive(false);
        Mmenu_button.SetActive(false);

        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    // Toggle pause on button click
    public void OnPauseButtonClick()
    {
        if (!isPaused)
        {
            ShowPause();  
        }
        else
        {
            HidePauseMenu(); 
        }
    }

    // Resume the game
    public void OnResumeButtonClick()
    {
        HidePauseMenu();
    }

    // Restart button action: Move player to respawn point
    public void OnRestartButton()
    {
        // Move the player to the respawn position
        player.transform.position = respawn.transform.position;
        Debug.Log("Player has been respawned at: " + respawn.transform.position);
        HidePauseMenu();
        Time.timeScale = 1f;
    }
    
}
