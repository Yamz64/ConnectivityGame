using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class CharacterMovement : MonoBehaviour
{
    public float network_horizontal;
    public float network_vertical;
    public bool network_action;

    public int conveyor_state;      //state of the character on a conveyor belt, if the player is not on a conveyor belt 0, if on right conveyor belt 1, if on left conveyor belt 2
    public float move_speed;
    public float jump_speed;
    public float conveyor_speed;
    public float super_jump_speed;
    public float jump_timer;
    public float gravity_scale;
    public bool can_jump;
    public bool grounded = false;
    public bool flipped;
    public bool super_jump;
    public bool super_lock;

    private float max_jump_timer;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    //--ACCESSORS--
    public float GetMaxJump() { return max_jump_timer; }
    public Rigidbody2D GetRB() { return rb; }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //function handles jumping and vertical movement
    public virtual void JumpFunc(bool jumpable = true)
    {
        if (jumpable)
        {
            if (grounded)
            {
                jump_timer = max_jump_timer;
                if (!super_lock) super_jump = false;
            }

            //jump is inputted
            if (network_vertical > 0.0f)
            {
                if (jump_timer > 0.0f)
                {
                    jump_timer -= 1.0f * Time.deltaTime;
                    if (!super_jump)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jump_speed);
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x, super_jump_speed);
                    }
                }
            }
            //Jump is released
            else if (network_vertical == 0.0f)
            {
                if (!grounded)
                {
                    jump_timer = 0.0f;
                }
            }
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        max_jump_timer = jump_timer;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity_scale;

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        //misc
        network_vertical = Input.GetAxis("Vertical");
        network_horizontal = Input.GetAxis("Horizontal");
        network_action = Input.GetButtonDown("Action");

        //--MOVEMENT--
        //--handle vertical movement--
        JumpFunc(can_jump);

        //--handle horizontal movement--
        switch (conveyor_state) {
            case 0:
                rb.velocity = new Vector2(network_horizontal * move_speed, rb.velocity.y);
                break;
            case 1:
                rb.velocity = new Vector2(network_horizontal * move_speed + conveyor_speed, rb.velocity.y);
                break;
            case 2:
                rb.velocity = new Vector2(network_horizontal * move_speed - conveyor_speed, rb.velocity.y);
                break;
            default:
                rb.velocity = new Vector2(network_horizontal * move_speed, rb.velocity.y);
                break;
        }

        //--VISUAL--
        //--handle sprite flipping
        if (network_horizontal > 0.0f)
        {
            flipped = false;
        }
        else if (network_horizontal < 0.0f)
        {
            flipped = true;
        }
            sprite.flipX = flipped;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Hazards" || other.tag == "Enemy")
        {
            Die();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag != "Little One" && other.tag != "Big One")
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Little One" && other.tag != "Big One")
        grounded = false;
    }
}
