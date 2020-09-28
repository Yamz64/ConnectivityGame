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
            Destroy(gameObject);
        }
    }
}
