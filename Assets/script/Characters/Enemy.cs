using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float vel;
    public Animator animator;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.left * vel;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetBool("Dead", true);
            Invoke("Destroy_Enemy", 3.0f); 
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("Destroy_Enemy", 3.0f);
    }
    private void Destroy_Enemy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
