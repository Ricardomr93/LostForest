using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    private float horizMove = 0;
    private float vertMove = 0;
    public Joystick joystick;

    private bool jumping = false;
    private int lives;
    private int coins;
    private bool dead;
    private bool damage;
    private bool movRig;
    private bool movLef;
    private AudioSource pjAudioSource;
    private Rigidbody2D rb2d;


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
    //clips
    public AudioClip jumpClip, attackClip, hitClip, runClip, coinClip, potionClip;


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
        vertMove = joystick.Vertical * runSpeedHorizontal;
        transform.position += new Vector3(horizMove, 0, 0) * Time.deltaTime * velPlayerRig;
        transform.position += new Vector3(vertMove, 0, 0) * Time.deltaTime * velPlayerJump;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("PJ_Attack");

        if (horizMove<0)
        {
            Run(-velPlayerLef, true);
        }
        else if (horizMove>0)
        {
            Run(velPlayerRig, false);
        }
        //no move
        else
        {
            animator.SetBool("Run", false);
        }

        if (vertMove>0 && !jumping)
        {
            Debug.Log("Pulsa W");
            Jump();

        }
        realJump();

        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            Attack();
        }

        if (attacking)
        {
            Debug.Log("Vuelve a recibir daño");
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
            animator.SetBool("Falling", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin") && !dead)
        {
            pjAudioSource.PlayOneShot(coinClip);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            coins++;
        }
        if (collision.gameObject.CompareTag("Potion") && !dead)
        {
            pjAudioSource.PlayOneShot(potionClip);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            lives++;
        }
        if (collision.gameObject.CompareTag("Enemy") && damage && !dead)
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
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
        pjAudioSource.clip = attackClip;
        pjAudioSource.Play();
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
            if (!pjAudioSource.isPlaying)
            {
                pjAudioSource.PlayOneShot(runClip);
            }
            animator.SetBool("Run", true);
        }

    }
    public void MoveRig(bool act)
    {
        movRig = act;
    }
    public void Moveleft(bool act)
    {
        movLef = act;
    }
}
