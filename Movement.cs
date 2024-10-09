using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private float moveInput;

    public float jump;
    public bool isJumping = true;
    private Rigidbody2D rb;

    public AudioSource audioSource;  // AudioSource to play sounds
    public AudioClip jump_audio;     // AudioClip for jump sound

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle movement
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump); 
            isJumping = true;  
            Debug.Log("jump");

            // Play jump sound using the AudioClip, not the AudioSource
            if (audioSource != null && jump_audio != null)
            {
                audioSource.PlayOneShot(jump_audio);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // When the player lands on the ground, allow jumping again
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;  
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // When the player is not on the ground, set isJumping to true
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
}
