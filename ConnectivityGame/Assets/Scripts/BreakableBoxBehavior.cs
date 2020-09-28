using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBoxBehavior : MonoBehaviour
{
    public bool destructible;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (destructible)
        {
            if (other.gameObject.tag != "Big One")
            {
                if (transform.root != null) Destroy(transform.root.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
