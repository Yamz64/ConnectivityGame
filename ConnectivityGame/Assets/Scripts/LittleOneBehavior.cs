using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleOneBehavior : CharacterMovement
{
    public bool active;
    public float transition_threshold;
    public bool held;
    public bool extra_jump;
    public bool used_extra_jump;
    public bool activate;

    private Animator anim;

    public override void JumpFunc(bool jumpable = true)
    {
        if (jumpable)
        {
            if (grounded)
            {
                extra_jump = true;
                used_extra_jump = false;
                jump_timer = GetMaxJump();
                if (!super_lock) super_jump = false;
                GetComponent<CharacterMovement>().GetSounds()[1].Play();
            }

            if (!controller_mode)
            {
                //jump is inputted
                if (network_vertical > 0.0f)
                {
                    if (jump_timer > 0.0f)
                    {
                        jump_timer -= 1.0f * Time.deltaTime;
                        if (!super_jump)
                        {
                            GetRB().velocity = new Vector2(GetRB().velocity.x, jump_speed);
                        }
                        else
                        {
                            if (extra_jump)
                            {
                                GetRB().velocity = new Vector2(GetRB().velocity.x, super_jump_speed);
                            }
                            else
                            {
                                GetRB().velocity = new Vector2(GetRB().velocity.x, jump_speed);
                            }
                        }
                    }
                    if (!extra_jump) used_extra_jump = true;
                }
                //Jump is released
                else if (network_vertical == 0.0f)
                {
                    if (!grounded)
                    {
                        if (!extra_jump && used_extra_jump)
                        {
                            jump_timer = 0.0f;
                        }
                        else
                        {
                            if (!used_extra_jump)
                            {
                                jump_timer = GetMaxJump();
                                extra_jump = false;
                            }
                        }
                    }
                }
            }
            else
            {
                //jump is inputted
                if (Input.GetButton("JoyJump"))
                {
                    if (jump_timer > 0.0f)
                    {
                        jump_timer -= 1.0f * Time.deltaTime;
                        if (!super_jump)
                        {
                            GetRB().velocity = new Vector2(GetRB().velocity.x, jump_speed);
                        }
                        else
                        {
                            if (extra_jump)
                            {
                                GetRB().velocity = new Vector2(GetRB().velocity.x, super_jump_speed);
                            }
                            else
                            {
                                GetRB().velocity = new Vector2(GetRB().velocity.x, jump_speed);
                            }
                        }
                    }
                    if (!extra_jump) used_extra_jump = true;
                }
                //Jump is released
                else if (Input.GetButtonUp("JoyJump"))
                {
                    if (!grounded)
                    {
                        if (!extra_jump && used_extra_jump)
                        {
                            jump_timer = 0.0f;
                        }
                        else
                        {
                            if (!used_extra_jump)
                            {
                                jump_timer = GetMaxJump();
                                extra_jump = false;
                            }
                        }
                    }
                }
            }
        }
    }

    void Animate()
    {
        //walk animation
        anim.SetBool("Walk", Mathf.Abs(network_horizontal) > 0.0f && !held);

        //Handle Aerial Movement
        if (grounded)
        {
            anim.SetInteger("JumpState", 0);
        }
        else
        {
            //if not moving vertically fast enough
            if (Mathf.Abs(base.GetRB().velocity.y) <= transition_threshold)
            {
                anim.SetInteger("JumpState", 2);
            }
            //if moving vertically fast enough
            else
            {
                //if moving up
                if (Mathf.Abs(base.GetRB().velocity.y) / base.GetRB().velocity.y == 1)
                {
                    anim.SetInteger("JumpState", 1);
                }
                //if moving down
                else
                {
                    anim.SetInteger("JumpState", 3);
                }
            }
        }
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        extra_jump = true;
        used_extra_jump = false;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    new void Update()
    {
        if (active)
        {
            if (!held)
            {
                base.Update();
            }
            else
            {
                network_vertical = Input.GetAxis("Vertical");
                if (network_vertical > 0.0f) GameObject.FindGameObjectWithTag("Big One").GetComponent<BigOneBehavior>().stop_grab = true;
            }

            if (grounded) held = false;
            Animate();
        }
    }
}
