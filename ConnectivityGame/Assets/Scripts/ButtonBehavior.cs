using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public bool controller_mode;
    public bool in_trigger;

    public Color color;
    public GameObject affected;
    private SpriteRenderer rend;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        rend.color = color;
    }

    private void Update()
    {
        if (affected != null)
        {
            affected.GetComponent<SpriteRenderer>().color = color;

            if (!controller_mode)
            {
                if (in_trigger && Input.GetButtonDown("Action"))
                {
                    source.Play();
                    affected.GetComponent<ButtonParent>().Activate();
                }
            }
            else
            {
                if (in_trigger && Input.GetButtonDown("JoyAction"))
                {
                    source.Play();
                    affected.GetComponent<ButtonParent>().Activate();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Little One")
        {
            controller_mode = other.GetComponent<CharacterMovement>().controller_mode;
            in_trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Little One")
        {
            in_trigger = false;
        }
    }
}
