using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class spike_code : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gameover;
    public GameObject yes_button;
    public GameObject no_button;
    public GameObject try_again;
    
    public GameObject respawn;
    private GameObject player;

    public AudioClip gameOverSound;  
    private AudioSource audioSource;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        if (yes_button != null) {
            Button yesBt = yes_button.GetComponent<Button>();
            yesBt.onClick.AddListener(OnYesButtonClick);
        }
        if (no_button != null){
            Button noBt = no_button.GetComponent<Button>();
            noBt.onClick.AddListener(OnNoButton);
        }
        audioSource = GetComponent<AudioSource>();
        
}
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Movement catmove = other.GetComponent<Movement>();
            Rigidbody2D cat_Rb = other.GetComponent<Rigidbody2D>();
            if(catmove != null && cat_Rb != null){
                catmove.speed = 0f;
                catmove.jump = 0f;

                cat_Rb.velocity = Vector2.zero;
                cat_Rb.angularVelocity = 0f;
                cat_Rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                if(Gameover != null) Gameover.SetActive(true);
                if(try_again != null) try_again.SetActive(true);
                if(yes_button != null) yes_button.SetActive(true);
                if(no_button != null) no_button.SetActive(true);

                if (gameOverSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(gameOverSound);
                }


            }
        }
    }


    void OnYesButtonClick()
    {
        if(audioSource != null && audioSource.isPlaying){
            audioSource.Stop();
        }
        if(player != null && respawn != null){
            player.transform.position = respawn.transform.position;
            if(Gameover != null) Gameover.SetActive(false);
            if(try_again != null) try_again.SetActive(false);
            if(yes_button != null) yes_button.SetActive(false);
            if(no_button != null) no_button.SetActive(false);

            Movement catmove = player.GetComponent<Movement>();
            Rigidbody2D cat_Rb = player.GetComponent<Rigidbody2D>();

        if (catmove != null && cat_Rb != null){

            catmove.speed = 10f;
            catmove.jump = 9;

            cat_Rb.constraints = RigidbodyConstraints2D.None;
            cat_Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
    void OnNoButton(){
        SceneManager.LoadScene("LEVEL2");
    }
}
