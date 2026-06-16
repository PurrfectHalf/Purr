using System;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    private Animator Flappy;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        Flappy = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;

            // --- ZIPLAMA SESÝ TETÝKLEYÝCÝSÝ ---
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.ziplamaSesi);
            }

            if (Flappy != null)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Flappy.SetTrigger("Fly");
                }
            }
        }

        if (transform.position.y > 12 || transform.position.y < -12)
        {
            if (birdIsAlive)
            {
                // --- EKRANDAN ÇIKIP YANMA SESÝ TETÝKLEYÝCÝSÝ ---
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlaySFX(AudioManager.instance.yanmaSesi);
                }

                birdIsAlive = false;
                logic.gameOver();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive)
        {
            // --- BORUYA ÇARPIP YANMA SESÝ TETÝKLEYÝCÝSÝ ---
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.yanmaSesi);
            }

            birdIsAlive = false;
            logic.gameOver();
        }
    }
}