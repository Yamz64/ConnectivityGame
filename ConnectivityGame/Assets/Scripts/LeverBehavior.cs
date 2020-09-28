using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public bool in_trigger;
    public bool activated;
    public Color lever_color;
    public GameObject affected_object;
    public Sprite on;
    public Sprite off;

    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.color = lever_color;
        activated = false;
        rend.sprite = off;
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
                    rend.sprite = on;
                    if (affected_object != null) affected_object.GetComponent<ToggleItemParent>().toggled = true;
                }
                else
                {
                    rend.sprite = off;
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
}
