using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finishline : MonoBehaviour
{
    public GameObject youwin; 
    public GameObject tap_continue; 

    public GameObject Back_button;
    public GameObject Next_button;
    public GameObject nextlevel;
    
    private bool tap_screen = false;  // To check if the screen has been tapped
    private bool playerReachedFish = false; // To check if player has touched the fish

    public AudioClip finish_sound;  
    private AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Access the player's movement script and disable it or stop movement
            Movement catMovement = other.GetComponent<Movement>();
            Rigidbody2D catRb = other.GetComponent<Rigidbody2D>();

            if (catMovement != null && catRb != null)
            {
                // Stop the player's movement
                catMovement.speed = 0f;
                catMovement.jump = 0f;

                // Set velocity to zero to stop the current movement
                catRb.velocity = Vector2.zero;
                catRb.angularVelocity = 0f;
                catRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                // Show the "YOU WIN!" and "Tap to Continue" messages
                if (youwin != null) youwin.SetActive(true);
                if (tap_continue != null) tap_continue.SetActive(true);

                // Set flag to indicate the player has reached the fish
                playerReachedFish = true;

                if (finish_sound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(finish_sound);
                }
            }
        }
    }

    void Update()
    {
        // Only allow clicking after the player has touched the fish
        if (playerReachedFish && Input.GetMouseButtonDown(0) && !tap_screen)
        {
            // Hide "YOU WIN!" and "Tap to Continue"
            if (youwin != null) youwin.SetActive(false);
            if (tap_continue != null) tap_continue.SetActive(false);

            // Show "Next Level" and buttons only after player reaches the fish
            if (nextlevel != null) nextlevel.SetActive(true);
            if (Next_button != null) Next_button.SetActive(true);
            if (Back_button != null) Back_button.SetActive(true);

            tap_screen = true;
        }
    }
}
