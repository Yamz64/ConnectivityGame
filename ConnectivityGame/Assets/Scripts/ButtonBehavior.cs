using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public bool in_trigger;

    public Color color;
    public GameObject affected;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.color = color;
    }

    private void Update()
    {


        if (affected != null)
        {
            affected.GetComponent<SpriteRenderer>().color = color;

            if (in_trigger && Input.GetButtonDown("Action"))
            {
                affected.GetComponent<ButtonParent>().Activate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Little One")
        {
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
