using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOneBehavior : CharacterMovement
{
    public float horizontal_grabrange;
    public float vertical_grabrange;
    public float throw_force;
    private Vector3 held_object_transform;      //position of held object starting the game
    private Vector3 thrown_object_transform;    //position of the thrown object starting the game
    private GameObject held_object_position;    //current position of held object
    private GameObject thrown_object_position;  //current position of thrown object

    private GameObject held_object;

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
                if (Input.GetButtonDown("Action"))
                {
                    //raycast in particular direction
                    bool valid = false;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, horizontal_grabrange);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.tag == "Box")
                        {
                            valid = true;
                            held_object = hit.collider.gameObject;
                            held_object.GetComponent<Collider2D>().enabled = false;
                            held_object.GetComponent<Rigidbody2D>().isKinematic = true;
                        }
                    }
                    //check below the character for a valid object if nothing is found to the side
                    if (!valid)
                    {
                        hit = Physics2D.Raycast(transform.position, Vector2.down, horizontal_grabrange);
                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.tag == "Box")
                            {
                                held_object = hit.collider.gameObject;
                                held_object.GetComponent<Collider2D>().enabled = false;
                                held_object.GetComponent<Rigidbody2D>().isKinematic = true;
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
                if (Input.GetButtonDown("Action"))
                {
                    if (!(Input.GetAxis("Vertical") < 0.0f))
                    {
                        held_object.transform.position = thrown_object_position.transform.position;
                    }
                    else
                    {
                        held_object.transform.position = new Vector2(thrown_object_position.transform.position.x, transform.position.y);
                    }
                    held_object.GetComponent<Collider2D>().enabled = true;
                    held_object.GetComponent<Rigidbody2D>().isKinematic = false;
                    if(!(Input.GetAxis("Vertical") < 0.0f))
                    held_object.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1.0f, 1.0f) * held_object.GetComponent<Rigidbody2D>().mass * throw_force);
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
                if (Input.GetButtonDown("Action"))
                {
                    //raycast in particular direction
                    bool valid = false;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, horizontal_grabrange);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.tag == "Box")
                        {
                            valid = true;
                            held_object = hit.collider.gameObject;
                            held_object.GetComponent<Collider2D>().enabled = false;
                            held_object.GetComponent<Rigidbody2D>().isKinematic = true;
                        }
                    }
                    //check below the character for a valid object if nothing is found to the side
                    if (!valid)
                    {
                        hit = Physics2D.Raycast(transform.position, Vector2.down, horizontal_grabrange);
                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.tag == "Box")
                            {
                                held_object = hit.collider.gameObject;
                                held_object.GetComponent<Collider2D>().enabled = false;
                                held_object.GetComponent<Rigidbody2D>().isKinematic = true;
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
                if (Input.GetButtonDown("Action"))
                {
                    if (!(Input.GetAxis("Vertical") < 0.0f))
                    {
                        held_object.transform.position = thrown_object_position.transform.position;
                    }
                    else
                    {
                        held_object.transform.position = new Vector2(thrown_object_position.transform.position.x, transform.position.y);
                    }
                    held_object.GetComponent<Collider2D>().enabled = true;
                    held_object.GetComponent<Rigidbody2D>().isKinematic = false;
                    if(!(Input.GetAxis("Vertical") < 0.0f))
                    held_object.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.0f, 1.0f) * held_object.GetComponent<Rigidbody2D>().mass * throw_force);
                    held_object = null;
                    can_jump = true;
                }
            }
        }

        //set the current held object's position to the held object's transform
        if (held_object != null)
            held_object.transform.position = held_object_position.transform.position;
    }

    new void Start()
    {
        base.Start();
        held_object_position = transform.GetChild(0).gameObject;
        held_object_transform = held_object_position.transform.localPosition;

        thrown_object_position = transform.GetChild(1).gameObject;
        thrown_object_transform = thrown_object_position.transform.localPosition;

        held_object = null;
    }

    new void Update()
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
    }
}
