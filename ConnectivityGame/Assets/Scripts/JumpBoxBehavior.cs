using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoxBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Big One" || other.tag == "Little One")
        {
            other.GetComponent<CharacterMovement>().super_lock = true;
            other.GetComponent<CharacterMovement>().super_jump = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Big One" || other.tag == "Little One")
        {
            other.GetComponent<CharacterMovement>().super_lock = false;
            other.GetComponent<CharacterMovement>().super_jump = true;
        }
    }
}
