using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;
   

    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    private bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb2d.AddForce(transform.up * 200);
        anim.SetTrigger("Flap");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false && GameControl.instance.isPaused == false)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
            {
                rb2d.velocity = Vector2.zero; // reset para consistent yung jump
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
                GameControl.instance.audioSource.PlayOneShot(GameControl.instance.flapSound, 0.5f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hasCollided == false)
        {
            rb2d.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Dead");
            hasCollided = true;
            GameControl.instance.audioSource.PlayOneShot(GameControl.instance.hitSound, 0.5f);

            GameControl.instance.playerCount -= 1;

            if (GameControl.instance.playerCount <= 0)
            {
                GameControl.instance.BirdDied();
            }

            if (isDead)
            {
                Destroy(gameObject, GameControl.instance.destroyBirdsAfter);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup") && !GameControl.instance.gameOver && hasCollided == false)
        {
            Destroy(collision.gameObject);
            GameControl.instance.audioSource.PlayOneShot(GameControl.instance.startSound, 0.5f);
            //Instantiate(GameControl.instance.bird, (Vector2)transform.position + new Vector2(Random.Range(0f, 1.5f), Random.Range(0f,2f)), transform.rotation);
            GameControl.instance.playerCount += 1;

            GameObject newBird = Instantiate(GameControl.instance.bird, (Vector2)transform.position + new Vector2(Random.Range(0f, 1.5f), Random.Range(-2f, 0f)), transform.rotation) as GameObject;
            newBird.GetComponent<Rigidbody2D>().AddForce(newBird.transform.up * 50);
            newBird.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            newBird.GetComponent<Animator>().SetTrigger("Flap");
        }
    }
}
