using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int health = 3;
    public int lives = 3;

    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    public float maxSpeed = 2f; // Changed to PascalCase (Unity convention)
    public int damage1;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Flip()
    {
        sr.flipX = !sr.flipX;
    }

    void Update()
    {
        if (isImmune)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;

            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                sr.enabled = true;
            }
        }
    }

    void SpriteFlicker()
    {
        if (flickerTime < flickerDuration)
        {
            flickerTime += Time.deltaTime;
        }
        else
        {
            sr.enabled = !sr.enabled;
            flickerTime = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            health -= damage;
            if (health < 0)
                health = 0;

            if (lives > 0 && health == 0)
            {
                FindObjectOfType<LevelManager>().RespawnPlayer();
                health = 3;
                lives--;
            }
            else if (lives == 0 && health == 0)
            {
                Debug.Log("Game Over");
                Destroy(gameObject);
            }

            Debug.Log("Player Health: " + health);
            Debug.Log("Player Lives: " + lives);

            isImmune = true;
            immunityTime = 0f;
        }
    }
}
