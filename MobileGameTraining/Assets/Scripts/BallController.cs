using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    public float bounceForce;
    bool gameStarted;
    

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown)
            {
                StartBounce();
                gameStarted = true;
                GameManager.instance.GameStart();
            }
        }
       
    }
    
    void StartBounce()
    {
       Vector2 randomDirection = new Vector2(Random.Range(-1, 1), 1);
       rb.AddForce(randomDirection * bounceForce, ForceMode2D.Impulse);
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem hitParticles = GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>();
        AudioSource audio = GameObject.Find("Metalic Impact").GetComponent<AudioSource>();
        audio.Stop();
        var main = hitParticles.main;
        hitParticles.Stop();
        main.duration = 2.0f;
        main.loop = false;
        if (collision.gameObject.tag == "FallCheck")
        {
            hitParticles.Stop();
            GameManager.instance.Restart();
            
        }
        else if(collision.gameObject.tag == "Paddle")
        {
            hitParticles.Play();
            audio.Play();
            GameManager.instance.ScoreUp();
        }
    }
}

