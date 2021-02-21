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
    private bool movRig;
    private bool movLef;
    private bool attacking;
    private AudioSource pjAudioSource;
    private Rigidbody2D rb2d;
    private float horizMove = 0;
    private float vertMove = 0;
    public Joystick joystick;
    public float runSpeedHorizontal;
    public float runSpeedVertical;
    public GameObject pj;
    public Animator animator;
    public float velPlayerLef;
    public float velPlayerRig;
    public float velPlayerJump;
    public Collider2D attackCol;
    public bool realJumping;
    public float fallMultipler;
    public float lowMultipler;
    public AudioSource swordSource;
    //clips
    public AudioClip jumpClip, attackClip, hitClip, coinClip, potionClip;
    public AudioClip[] runClip;



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

    public bool Dead
    {
        get { return dead; }
        set { dead = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coins = 0;
        lives = 3;
        dead = false;
        damage = true;
        pjAudioSource = GetComponent<AudioSource>();
        attackCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizMove = joystick.Horizontal * runSpeedHorizontal;
        vertMove = joystick.Vertical * runSpeedVertical;
        transform.position += new Vector3(horizMove, 0, 0) * Time.deltaTime * velPlayerRig;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsName("PJ_Attack");

        if (Input.GetKey(KeyCode.A) || horizMove < 0)
        {
            Run(-velPlayerLef, true);
        }
        else if (Input.GetKey(KeyCode.D) || horizMove > 0)
        {
            Run(velPlayerRig, false);
        }
        //no move
        else
        {
            rb2d.velocity = new Vector2(-2, rb2d.velocity.y);
            animator.SetBool("Run", false);
        }

        //if (Input.GetKey(KeyCode.W) || vertMove > 0 && !jumping)
        if (vertMove > 0.2 && !jumping)
        {
            Jump();
        }
        else if (vertMove < 0)
        {

        }
        // realJump();

        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            Attack();
        }

        if (attacking)
        {
            damage = false;
            Invoke("no_Damage", 2.0f);
            attackCol.enabled = true;
        }
        else attackCol.enabled = false;
        if (rb2d.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
        }
        else if (rb2d.velocity.y > 0)
        {
            animator.SetBool("Falling", false);
        }
        //if isn't in the air and is dead
        if (!jumping && dead)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }
    void realJump()
    {
        if (realJumping)
        {
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipler) * Time.deltaTime;
            }
            if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.W))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowMultipler) * Time.deltaTime;
            }
        }
    }
    void no_Damage()
    {
        damage = true;
    }
    void OnCollisionEnter2D(Collision2D _col)
    {
        //Entra si dos objetos colisionan por primera vez
        if (_col.gameObject.CompareTag("Ground"))
        {
            jumping = false;
            animator.SetBool("Jump", jumping);
            animator.SetBool("Falling", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin") && !dead) coins++;
        if (collision.gameObject.CompareTag("Potion") && !dead) lives++;
 
        if (collision.gameObject.CompareTag("Enemy") && damage && !dead)
        {
            //collision.gameObject.GetComponent<Collider2D>().enabled = false;
            damage = false;
            Invoke("no_Damage", 1.0f);
            lives--;
            pjAudioSource.clip = hitClip;
            pjAudioSource.Play();
            animator.SetTrigger("Hit");
            if (lives <= 0)
            {
                animator.SetBool("Dead", true);
                dead = true;
            }
        }
    }
    public void Attack()
    {
        transform.Translate(new Vector3(-0.1f, 0.0f));
        animator.SetTrigger("Attack");
        swordSource.Play();
    }
    public void Jump()
    {
        if (!dead)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, velPlayerJump);
            jumping = true;
            animator.SetBool("Jump", jumping);
            pjAudioSource.PlayOneShot(jumpClip);
        }
    }
    private void Run(float dire, bool flip)
    {
        if (!dead)
        {
            rb2d.velocity = new Vector2(dire, rb2d.velocity.y);
            pj.GetComponent<SpriteRenderer>().flipX = flip;
            if (!pjAudioSource.isPlaying && !jumping)
            {
                var step = Random.Range(0, runClip.Length);
                pjAudioSource.PlayOneShot(runClip[step]);
                Debug.Log(step);
            }
            animator.SetBool("Run", true);
        }

    }
}
