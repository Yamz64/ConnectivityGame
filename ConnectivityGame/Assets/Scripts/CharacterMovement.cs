using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class CharacterMovement : MonoBehaviour
{
    public Color invincible_color;

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
    public float camera_speed;
    public bool controller_mode;
    public bool can_jump;
    public bool grounded = false;
    public bool flipped;
    public bool super_jump;
    public bool super_lock;
    public bool cam_mode;           //determines the state that the camera is in, if this is false it is in follow cam, if it is in true it views the whole level
    public Camera cam;


    private float max_jump_timer;
    private bool invincible;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private AudioSource[] sounds;

    //--ACCESSORS--
    public float GetMaxJump() { return max_jump_timer; }
    public Rigidbody2D GetRB() { return rb; }
    public AudioSource[] GetSounds() { return sounds; }

    IEnumerator Invincibility_Routine(float duration)
    {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }

    IEnumerator LerpCam()
    {
        float current_time = 0;
        while(current_time != 1)
        {
            current_time += camera_speed / 60f;
            yield return new WaitForSeconds(1 / 60f);
            if (current_time > 1.0f) current_time = 1.0f;
            cam.transform.position = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, transform.position.y, Mathf.Abs(cam.transform.position.y - transform.position.y) * current_time), cam.transform.position.z);
        }
    }

    public void MakeInvincible(float duration)
    {
        StartCoroutine(Invincibility_Routine(duration));
    }

    public void Die()
    {
        if(!invincible)
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
                sounds[1].Play();
            }

            //jump is inputted
            if (!controller_mode)
            {
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
            else
            {
                if (Input.GetButton("JoyJump"))
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
                else if (Input.GetButtonDown("JoyJump"))
                {
                    if (!grounded)
                    {
                        jump_timer = 0.0f;
                    }
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
        sounds = GetComponents<AudioSource>();
        cam.transform.parent = null;
    }

    // Update is called once per frame
    public void Update()
    {
        //misc
        if (!controller_mode)
        {
            network_vertical = Input.GetAxis("Vertical");
            network_horizontal = Input.GetAxis("Horizontal");
            network_action = Input.GetButtonDown("Action");
        }
        else
        {
            network_vertical = Input.GetAxis("JoyVertical");
            network_horizontal = Input.GetAxis("JoyHorizontal");
            network_action = Input.GetButtonDown("JoyAction");
        }

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

        if (invincible)
        {
            sprite.color = invincible_color;
        }
        else
        {
            sprite.color = Color.white;
        }

        //Interp Cam
        if (!cam_mode)
        {
            cam.orthographicSize = 5;
            cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, -10f);
            StopCoroutine(LerpCam());
            StartCoroutine(LerpCam());
        }
        else
        {
            GameObject cam_origin = GameObject.FindGameObjectWithTag("LevelCam");
            cam.transform.position = new Vector3(cam_origin.transform.position.x, cam_origin.transform.position.y, -10f);
            cam.orthographicSize = cam_origin.transform.localScale.z;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Little One" && other.tag != "Big One" && other.tag != "Teleporter")
            sounds[2].Play();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag != "Little One" && other.tag != "Big One" && other.tag != "Teleporter")
        grounded = true;
        if (other.tag == "Hazards" || other.tag == "Enemy")
        {
            Die();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Little One" && other.tag != "Big One" && other.tag != "Teleporter")
        grounded = false;
    }
}
