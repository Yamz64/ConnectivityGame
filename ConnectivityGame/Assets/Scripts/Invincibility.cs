using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public float invincibility_duration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Little One" || other.tag == "Big One")
        {
            other.GetComponent<CharacterMovement>().MakeInvincible(invincibility_duration);
            Destroy(gameObject);
        }
    }
}
