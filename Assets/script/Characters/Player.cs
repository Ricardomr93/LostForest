using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumping = false;
    private int lives;
    private int coins;
    private bool dead;
    private bool damage;
    private AudioSource pj_as;


    public GameObject attck_pos;
    public GameObject pj;
    public Animator animator;
    public float vel_player_lef;
    public float vel_player_rig;
    public float vel_player_jump;
    public Collider2D attack_Col;

    //clips
    public AudioClip jump_clip, attack_clip, hit_clip, run_clip,sword_hit;



    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    public bool Damage
    {
        get { return damage; }
        set { damage = value; }
    }




    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        lives = 3;
        dead = false;
        damage = true;
        pj_as = GetComponent<AudioSource>();
        attack_Col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("PJ_Attack");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Run(-vel_player_lef, true);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Run(vel_player_rig, false);
        }
        //no move
        else
        {
            transform.Translate(new Vector3(-0.01f, 0.0f));
            animator.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            transform.Translate(new Vector3(-0.1f, 0.0f));
            animator.SetTrigger("Attack");
            pj_as.clip = attack_clip;
            pj_as.Play();
        }

        if (attacking)
        {
            Debug.Log("Vuelve a recibir daño");
            damage = false;
            Invoke("no_Damage", 2.0f);
            attack_Col.enabled = true;
            

        }
        else attack_Col.enabled = false;
    }
    void no_Damage()
    {
        Debug.Log("Vuelve a recibir daño");
        damage = true;
    }
    void OnCollisionEnter2D(Collision2D _col)
    {
        //Entra si dos objetos colisionan por primera vez
        if (_col.gameObject.CompareTag("Ground"))
        {
            jumping = false;
            animator.SetBool("Jump", jumping);
        }
        if (_col.gameObject.CompareTag("Enemy") && damage && !dead)
        {
            Debug.Log("choca contra enemigo");
            damage = false;
            Invoke("no_Damage", 2.0f);
            lives--;
            pj_as.clip = hit_clip;
            pj_as.Play();
            
            animator.SetTrigger("Hit");
            if (lives <= 0)
            {
                animator.SetBool("Dead", true);
                dead = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            coins++;
        }
    }
    private void Jump()
    {
        if (!jumping && !dead)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, vel_player_jump));
            jumping = true;
            animator.SetBool("Jump", jumping);
            pj_as.PlayOneShot(jump_clip);
        }
    }
    private void Run(float dire, bool flip)
    {
        if (!dead)
        {
            transform.Translate(new Vector3(dire, 0.0f));
            pj.GetComponent<SpriteRenderer>().flipX = flip;
            if (!pj_as.isPlaying)
            {
                pj_as.PlayOneShot(run_clip);
            }
            animator.SetBool("Run", true);
        }
       
    }
}
