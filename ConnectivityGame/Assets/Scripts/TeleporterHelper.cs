using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterHelper : MonoBehaviour
{
    public GameObject teleported_object;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Big One" || other.tag == "Little One" || other.tag == "Box" || other.tag == "BreakableBox" || other.tag == "TeleporterBox")
        {
            teleported_object = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Big One" || other.tag == "Little One" || other.tag == "Box" || other.tag == "BreakableBox" || other.tag == "TeleporterBox")
        {
            teleported_object = null;
        }
    }
}
