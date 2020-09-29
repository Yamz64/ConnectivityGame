using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public bool controller_mode;
    public bool in_trigger;
    public bool activated;
    public Color lever_color;
    public GameObject affected_object;

    private Animator anim;
    private SpriteRenderer rend;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        rend.color = lever_color;
        activated = false;
        if(affected_object != null)
        affected_object.GetComponent<SpriteRenderer>().color = lever_color;
    }

    private void Update()
    {
        if (in_trigger)
        {
            if (!controller_mode)
            {
                if (Input.GetButtonDown("Action"))
                {
                    activated = !activated;
                    if (activated)
                    {
                        anim.SetBool("On", true);
                        source.Play();
                        if (affected_object != null) affected_object.GetComponent<ToggleItemParent>().toggled = true;
                    }
                    else
                    {
                        anim.SetBool("On", false);
                        source.Play();
                        if (affected_object != null) affected_object.GetComponent<ToggleItemParent>().toggled = false;
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("JoyAction"))
                {
                    activated = !activated;
                    if (activated)
                    {
                        anim.SetBool("On", true);
                        source.Play();
                        if (affected_object != null) affected_object.GetComponent<ToggleItemParent>().toggled = true;
                    }
                    else
                    {
                        anim.SetBool("On", false);
                        source.Play();
                        if (affected_object != null) affected_object.GetComponent<ToggleItemParent>().toggled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Little One")
        {
            controller_mode = other.GetComponent<CharacterMovement>().controller_mode;
            in_trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Little One")
        {
            in_trigger = false;
        }
    }
}
