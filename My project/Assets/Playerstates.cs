using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public int health = 3;
    public int lives = 3;

    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    public float maxspeed = 2;
    public int damage1;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Flip()
    {
        sr.flipX = !sr.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmune == true)
        {
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;

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
            flickerTime = flickerTime + Time.deltaTime;
        }
        else if (flickerTime >= flickerDuration)
        {
            sr.enabled = !sr.enabled;
            flickerTime = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isImmune == false)
        {
            health = health - damage;
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
                Destroy(this.gameObject);
            }

            Debug.Log("Player Health: " + health.ToString());
            Debug.Log("Player Lives: " + lives.ToString());

            isImmune = true;
            immunityTime = 0f;
        }
    }
}
