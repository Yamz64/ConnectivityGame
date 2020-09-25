using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float move_speed;
    public float jump_speed;
    public float jump_timer;
    public float gravity_scale;
    public bool can_jump;
    public bool grounded = false;
    public bool flipped;

    private float max_jump_timer;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    //--ACCESSORS--
    public float GetMaxJump() { return max_jump_timer; }
    public Rigidbody2D GetRB() { return rb; }

    //function handles jumping and vertical movement
    public virtual void JumpFunc(bool jumpable = true)
    {
        if (jumpable)
        {
            if (grounded)
            {
                jump_timer = max_jump_timer;
            }

            //jump is inputted
            if (Input.GetAxis("Vertical") > 0.0f || Input.GetAxis("Jump") > 0.0f)
            {
                if (jump_timer > 0.0f)
                {
                    jump_timer -= 1.0f * Time.deltaTime;
                    rb.velocity = new Vector2(rb.velocity.x, jump_speed);
                }
            }
            //Jump is released
            else if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Jump") == 0.0f)
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
        //--MOVEMENT--
        //--handle vertical movement--
        JumpFunc(can_jump);

        //--handle horizontal movement--
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * move_speed, rb.velocity.y);

        //--VISUAL--
        //--handle sprite flipping
        if(Input.GetAxis("Horizontal") > 0.0f)
        {
            flipped = false;
        }
        else if(Input.GetAxis("Horizontal") < 0.0f)
        {
            flipped = true;
        }
        sprite.flipX = flipped;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        grounded = false;
    }
}
