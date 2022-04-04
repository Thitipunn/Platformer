using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 5f;
    public int hp = 3;
    private bool isFlash = false;
    private string currentAnimation;
    const float groundCheckRadius = 0.2f;

    //animation
    const string Player_idle = "ass_idle";
    const string Player_run = "ass_run";
    const string Player_jump = "ass_jump";
    const string Player_superjump = "ass_superjump";

    [SerializeField] private GameObject effect;
    [SerializeField] private Transform jetPack;


    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anime;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private bool canDoubleJump;
    [SerializeField] private bool isGround = false;
    [SerializeField] private bool isMoving;

    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        GroundCheck();
        Run();
        FlipSprite();
        //CheckGround();
        Jump();
        AnimePlayer();
    }

    void Run()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        isMoving = (Input.GetAxisRaw("Horizontal") != 0);
    }

    void FlipSprite()
    {
        bool flip = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (flip)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x),1f);
        }
    }

    void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(transform.position, 0.55f, groundLayer);
    }

    void Jump()
    {
        if (isGround)
        {
            canDoubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) &&!isFlash)
        {
            if (isGround)
            {
                ChangeAnimationState(Player_jump);
                rb.velocity = new Vector2(rb.velocity.x, jump);
                audioManager.instance.playJump();
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Space) && !isFlash)
                {
                    if (canDoubleJump)
                    {
                        ChangeAnimationState(Player_superjump);
                        Instantiate(effect,jetPack.transform.position,Quaternion.identity);
                        rb.velocity = new Vector2(rb.velocity.x, jump);
                        audioManager.instance.playJump();
                        canDoubleJump = false;
                    }
                }
            }
            
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }
    
    void AnimePlayer()
    {
        if (isGround)
        {
            if(rb.velocity.x == 0)
            {
                ChangeAnimationState(Player_idle);
            }
            else
            {
                ChangeAnimationState(Player_run);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !isFlash)
        {
            getDamage();
        }
    }

    void getDamage()
    {
        hp--;
        audioManager.instance.playEnemyHit();
        isFlash = true;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Flash());

        if(hp <= 0)
        {
            gameObject.SetActive(false);
            gamePlay.instance.gameOver();
        }
    }

    IEnumerator Flash()
    {
        for(int i =0; i < 6; i++)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        isFlash = false;
    }

    public void addHp()
    {
        hp++;
        if(hp > gamePlay.instance.maxHP)
        {
            hp = gamePlay.instance.maxHP;
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentAnimation == newState) return;

        anime.Play(newState);

        currentAnimation = newState;
    }
    
    void GroundCheck()
    {
        isGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRadius,groundLayer);
        if(colliders.Length > 0)
        {
            isGround = true;
        }
    }
}
