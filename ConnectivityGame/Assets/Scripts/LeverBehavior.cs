using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public bool in_trigger;
    public bool activated;
    public Color lever_color;
    public GameObject affected_object;

    private Animator anim;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rend.color = lever_color;
        activated = false;
        if(affected_object != null)
        affected_object.GetComponent<SpriteRenderer>().color = lever_color;
    }

    private void Update()
    {
        if (in_trigger)
        {
            if (Input.GetButtonDown("Action"))
            {
                activated = !activated;
                if (activated)
                {
                    anim.SetBool("On", true);
                    if (affected_object != null) affected_object.GetComponent<ToggleItemParent>().toggled = true;
                }
                else
                {
                    anim.SetBool("On", false);
                    if (affected_object != null) affected_object.GetComponent<ToggleItemParent>().toggled = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Little One")
        {
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
