using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public float life_time;

    IEnumerator Die_Routine()
    {
        yield return new WaitForSeconds(life_time);
        Destroy(gameObject);
    }

    public void Die() { StartCoroutine(Die_Routine()); }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Little One" || other.tag == "Big One")
        {
            other.GetComponent<CharacterMovement>().Die();
        }
        else if(other.tag == "Enemy" || other.tag == "BreakableBox")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
