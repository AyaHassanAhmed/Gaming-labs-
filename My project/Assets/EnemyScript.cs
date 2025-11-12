using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
        public float maxspeed=2;
        public int damage1;
        public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
         sr=GetComponent<SpriteRenderer>();
    }
    public void Flip()
    {
        sr.flipX=!sr.flipX;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
            if(other.tag=="Player")
            {
                FindObjectOfType<PlayerStates>().TakeDamage(damage1);
                Flip();
            }
            else if(other.tag=="wall")
            {
                Flip();
            }
        
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
