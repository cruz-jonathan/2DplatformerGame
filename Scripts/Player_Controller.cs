using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller instance;

    public float moveSpeed;
    public float jumpHeight;
    public float wallSlideSpeed;

    //Sliding Mechanics
    public float slideSpeed;
    public float slideTimer;
    public float slideTimerCurrent;

    private bool doubleJumped;
    private bool grounded;
    private bool crouch;
    private bool wallSlide;
    private bool falling;
    private bool wallJumped;
    private bool sliding;
    private bool direction;     //direction player is facing
    private bool headbonk;      //if player should not be standing up

    //Using new jump mechanics
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    public GameObject wallJumpIndicator;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Player is falling
        if (rb.velocity.y < 0)
        {
            falling = true;     //character has to be falling to be able to wall jump again
            //wallJumped = false; //character is only able to wall jump once
            
            //if character falls of while sliding
            //sliding = false;
            //slideTimerCurrent = 0;
        }

        if (slideTimerCurrent <= 0)
        {
            //If Player is on the ground
            if (rb.velocity.y < 0.005 && rb.velocity.y > -0.005)
            {
                grounded = true;
                doubleJumped = false;
                falling = false;
                wallSlide = false;
                wallJumped = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else
            {
                grounded = false;
            }

            //Movement
            if (!crouch)
            {
                rb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
            }

            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            //Crouching
            if (Input.GetKey("s") && grounded || headbonk && grounded)
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }

            //Slide
            if (Input.GetKeyDown("f") && crouch)
            {
                sliding = true;
                slide();
            }
            else
            {
                sliding = false;
            }

            //Jump
            if (Input.GetButtonDown("Jump") && grounded && !crouch)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }

            //Double Jumped
            if (Input.GetButtonDown("Jump") && !grounded && !doubleJumped && !wallSlide)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                doubleJumped = true;
            }

            //Wall Jump
            if (Input.GetButtonDown("Jump") && wallSlide && !wallJumped)
            {
                wallSlide = false;
                wallJumped = true;

                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }


            //if player is falling and Better Jump Mechanics
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            //Wall Slide -- Slide on the wall essentially
            if (wallSlide && !wallJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);
            }

            //Animation Stuff
            //Flip the sprite
            if (rb.velocity.x < 0 || Input.GetKey("a"))
            {
                //sr.flipX = true;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                direction = true;
            }
            else if (rb.velocity.x > 0 || Input.GetKey("d"))
            {
                //sr.flipX = false;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                direction = false;
            }
        }
        else
        {
            //Character Sliding
            slideTimerCurrent -= Time.deltaTime;
            if (!direction)
            {
                rb.velocity = new Vector2(slideSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-slideSpeed, rb.velocity.y);
            }
        }

        //Animation stuff
        anim.SetFloat("velocity", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("doubleJumped", doubleJumped);
        anim.SetBool("crouch", crouch);
        anim.SetBool("wallSlide", wallSlide);
        anim.SetBool("sliding", sliding);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "JumpGem")
        {
            Destroy(other.gameObject);
            doubleJumped = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Wall" && falling && !doubleJumped)
        {
            wallSlide = true;
        }
        //If player is under a tightspace
        if (other.tag == "Wall" && crouch && grounded)
        {
            headbonk = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            wallSlide = false;
            headbonk = false;
        }
    }

    public void slide()
    {
        slideTimerCurrent = slideTimer;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag=="MovingPlatform")
        {
            transform.parent = null;
        }
    }
}


