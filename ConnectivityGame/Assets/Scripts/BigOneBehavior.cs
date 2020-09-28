using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOneBehavior : CharacterMovement
{
    public bool active;
    public bool stop_grab;
    public float horizontal_grabrange;
    public float vertical_grabrange;
    public float throw_force;
    public float vertical_coefficient;
    public float transition_threshold;          //amount of vertical speed needed to transition to another state of vertical animation
    private Vector3 held_object_transform;      //position of held object starting the game
    private Vector3 thrown_object_transform;    //position of the thrown object starting the game
    private GameObject held_object_position;    //current position of held object
    private GameObject thrown_object_position;  //current position of thrown object

    public GameObject held_object;
    private Animator anim;

    public void EscapeHandler()
    {
        if (GameObject.FindGameObjectWithTag("Little One")) GameObject.FindGameObjectWithTag("Little One").GetComponent<LittleOneBehavior>().held = false;
        GameObject.FindGameObjectWithTag("Little One").GetComponents<Collider2D>()[0].enabled = true;
        GameObject.FindGameObjectWithTag("Little One").GetComponents<Collider2D>()[1].enabled = true;
        GameObject.FindGameObjectWithTag("Little One").GetComponent<Rigidbody2D>().isKinematic = false;
        held_object = null;
        can_jump = true;
        stop_grab = false;
    }

    //function that handles throwing objects
    void ThrowHandler()
{
        //handle throwing
        //to the left
        if (flipped)
        {
            //if not holding an object
            if (held_object == null)
            {
                if (network_action)
                {
                    //raycast in particular direction
                    bool valid = false;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.down * .3f), Vector2.left, horizontal_grabrange);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.tag == "Box" || hit.collider.gameObject.tag == "Little One" || hit.collider.gameObject.tag == "BreakableBox")
                        {
                            valid = true;
                            held_object = hit.collider.gameObject;
                            Collider2D[] colliders = held_object.GetComponents<Collider2D>();
                            foreach (Collider2D col in colliders)
                            {
                                col.enabled = false;
                            }
                            held_object.GetComponent<Rigidbody2D>().isKinematic = true;
                            if (held_object.tag == "Little One") held_object.GetComponent<LittleOneBehavior>().held = true;
                        }
                    }
                    //check below the character for a valid object if nothing is found to the side
                    if (!valid)
                    {
                        hit = Physics2D.Raycast(transform.position, Vector2.down, horizontal_grabrange);
                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.tag == "Box" || hit.collider.gameObject.tag == "Little One" || hit.collider.gameObject.tag == "BreakableBox")
                            {
                                held_object = hit.collider.gameObject;
                                Collider2D[] colliders = held_object.GetComponents<Collider2D>();
                                foreach (Collider2D col in colliders)
                                {
                                    col.enabled = false;
                                }
                                held_object.GetComponent<Rigidbody2D>().isKinematic = true;
                                if (held_object.tag == "Little One") held_object.GetComponent<LittleOneBehavior>().held = true;
                            }
                        }
                    }
                }
            }
            //if holding an object
            else
            {
                can_jump = false;
                //throw the object or put it down
                if (network_action)
                {
                    if (!(network_vertical < 0.0f))
                    {
                        held_object.transform.position = thrown_object_position.transform.position;
                    }
                    else
                    {
                        held_object.transform.position = new Vector2(thrown_object_position.transform.position.x, transform.position.y);
                    }
                    Collider2D[] colliders = held_object.GetComponents<Collider2D>();
                    foreach (Collider2D col in colliders)
                    {
                        col.enabled = true;
                    }
                    held_object.GetComponent<Rigidbody2D>().isKinematic = false;
                    if (network_vertical == 0.0f)
                    {
                        held_object.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1.0f, 1.0f) * held_object.GetComponent<Rigidbody2D>().mass * throw_force);
                        if (held_object.tag == "BreakableBox")
                        {
                            held_object.GetComponent<BreakableBoxBehavior>().destructible = true;
                            held_object.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        }
                    }
                    else if (network_vertical > 0.0f)
                    {
                        held_object.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 1.0f) * held_object.GetComponent<Rigidbody2D>().mass * throw_force * vertical_coefficient);
                        if (held_object.tag == "BreakableBox")
                        {
                            held_object.GetComponent<BreakableBoxBehavior>().destructible = true;
                            held_object.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        }
                    }
                    held_object = null;
                    can_jump = true;
                }
            }
        }
        //to the right
        else
        {
            //if not holding an object
            if (held_object == null)
            {
                if (network_action)
                {
                    //raycast in particular direction
                    bool valid = false;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.down * .3f), Vector2.right, horizontal_grabrange);
                    Debug.DrawRay(transform.position + Vector3.down * .3f, Vector2.right, Color.red, .5f);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.tag == "Box" || hit.collider.gameObject.tag == "Little One" || hit.collider.gameObject.tag == "BreakableBox")
                        {
                            valid = true;
                            held_object = hit.collider.gameObject;
                            Collider2D[] colliders = held_object.GetComponents<Collider2D>();
                            foreach(Collider2D col in colliders)
                            {
                                col.enabled = false;
                            }
                            held_object.GetComponent<Rigidbody2D>().isKinematic = true;
                            if (held_object.gameObject.tag == "Little One") held_object.GetComponent<LittleOneBehavior>().held = true;
                        }
                    }
                    //check below the character for a valid object if nothing is found to the side
                    if (!valid)
                    {
                        hit = Physics2D.Raycast(transform.position, Vector2.down, horizontal_grabrange);
                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.tag == "Box" || hit.collider.gameObject.tag == "Little One" || hit.collider.gameObject.tag == "BreakableBox")
                            {
                                held_object = hit.collider.gameObject;
                                Collider2D[] colliders = held_object.GetComponents<Collider2D>();
                                foreach (Collider2D col in colliders)
                                {
                                    col.enabled = false;
                                }
                                held_object.GetComponent<Rigidbody2D>().isKinematic = true;
                                if (held_object.gameObject.tag == "Little One") held_object.GetComponent<LittleOneBehavior>().held = true;
                            }
                        }
                    }
                }
            }
            //if holding an object
            else
            {
                can_jump = false;
                //throw the object or put it down
                if (network_action)
                {
                    if (!(network_vertical < 0.0f))
                    {
                        held_object.transform.position = thrown_object_position.transform.position;
                    }
                    else
                    {
                        held_object.transform.position = new Vector2(thrown_object_position.transform.position.x, transform.position.y);
                    }
                    Collider2D[] colliders = held_object.GetComponents<Collider2D>();
                    foreach (Collider2D col in colliders)
                    {
                        col.enabled = true;
                    }
                    held_object.GetComponent<Rigidbody2D>().isKinematic = false;
                    if (network_vertical == 0.0f)
                    {
                        held_object.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.0f, 1.0f) * held_object.GetComponent<Rigidbody2D>().mass * throw_force);
                        if (held_object.tag == "BreakableBox")
                        {
                            held_object.GetComponent<BreakableBoxBehavior>().destructible = true;
                            held_object.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        }
                    }
                    else if (network_vertical > 0.0f)
                    {
                        held_object.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 1.0f) * held_object.GetComponent<Rigidbody2D>().mass * throw_force * vertical_coefficient);
                        if (held_object.tag == "BreakableBox")
                        {
                            held_object.GetComponent<BreakableBoxBehavior>().destructible = true;
                            held_object.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        }
                    }
                    held_object = null;
                    can_jump = true;
                }
            }
        }

        //set the current held object's position to the held object's transform
        if (held_object != null)
            held_object.transform.position = held_object_position.transform.position;
    }

    void Animate()
    {
        //walk animation
        anim.SetBool("Walk", Mathf.Abs(network_horizontal) > 0.0f);
        anim.SetBool("Holding", held_object != null);

        //Handle Aerial Movement
        if (grounded)
        {
            anim.SetInteger("JumpState", 0);
        }
        else
        {
            //if not moving vertically fast enough
            if(Mathf.Abs(base.GetRB().velocity.y) <= transition_threshold)
            {
                anim.SetInteger("JumpState", 2);
            }
            //if moving vertically fast enough
            else
            {
                //if moving up
                if(Mathf.Abs(base.GetRB().velocity.y)/base.GetRB().velocity.y == 1)
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

    new void Start()
    {
        base.Start();
        held_object_position = transform.GetChild(0).gameObject;
        held_object_transform = held_object_position.transform.localPosition;

        thrown_object_position = transform.GetChild(1).gameObject;
        thrown_object_transform = thrown_object_position.transform.localPosition;

        held_object = null;

        anim = GetComponent<Animator>();
    }

    new void Update()
    {
        if (active)
        {
            base.Update();
            //handle flipping the held object
            if (flipped)
            {
                held_object_position.transform.localPosition = new Vector3(-held_object_transform.x, held_object_transform.y, held_object_transform.z);
                thrown_object_position.transform.localPosition = new Vector3(-thrown_object_transform.x, thrown_object_transform.y, thrown_object_transform.z);
            }
            else
            {
                held_object_position.transform.localPosition = new Vector3(held_object_transform.x, held_object_transform.y, held_object_transform.z);
                thrown_object_position.transform.localPosition = new Vector3(thrown_object_transform.x, thrown_object_transform.y, thrown_object_transform.z);
            }

            ThrowHandler();
            Animate();
            if (stop_grab) { EscapeHandler(); }
        }
    }
}
