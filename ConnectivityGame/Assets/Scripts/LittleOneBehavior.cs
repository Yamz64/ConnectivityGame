using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleOneBehavior : CharacterMovement
{
    public bool extra_jump;
    public bool used_extra_jump;

    public override void JumpFunc(bool jumpable = true)
    {
        if (jumpable)
        {
            if (grounded)
            {
                extra_jump = true;
                used_extra_jump = false;
                jump_timer = GetMaxJump();
            }

            //jump is inputted
            if (Input.GetAxis("Vertical") > 0.0f || Input.GetAxis("Jump") > 0.0f)
            {
                if (jump_timer > 0.0f)
                {
                    jump_timer -= 1.0f * Time.deltaTime;
                    GetRB().velocity = new Vector2(GetRB().velocity.x, jump_speed);
                }
                if (!extra_jump) used_extra_jump = true;
            }
            //Jump is released
            else if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Jump") == 0.0f)
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

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        extra_jump = true;
        used_extra_jump = false;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
