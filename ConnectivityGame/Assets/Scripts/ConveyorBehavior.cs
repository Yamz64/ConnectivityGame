using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBehavior : ToggleItemParent
{
    public float conveyor_speed;
    public bool direction;  //direction false = right, direction true = left

    public void AddForce(Rigidbody2D other, bool player = false)
    {
        if (player)
        {
            other.GetComponent<CharacterMovement>().conveyor_speed = conveyor_speed;
            if (!direction)
            {
                other.GetComponent<CharacterMovement>().conveyor_state = 1;
            }
            else
            {
                other.GetComponent<CharacterMovement>().conveyor_state = 2;
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<EnemyBehavior>().conveyor_speed = conveyor_speed;
                if (!direction)
                {
                    other.GetComponent<EnemyBehavior>().conveyor_state = 1;
                }
                else
                {
                    other.GetComponent<EnemyBehavior>().conveyor_state = 2;
                }
            }
            else
            {
                if (!direction)
                {
                    other.velocity += Vector2.right * conveyor_speed;
                }
                else
                {
                    other.velocity += Vector2.left * conveyor_speed;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Little One" || other.gameObject.tag == "Big One")
        {
            AddForce(other.gameObject.GetComponent<Rigidbody2D>(), true);
        }
        else if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Box" || other.gameObject.tag == "BreakableBox")
        {
            AddForce(other.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Little One" || other.gameObject.tag == "Big One")
        {
            other.gameObject.GetComponent<CharacterMovement>().conveyor_state = 0;
        }else if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyBehavior>().conveyor_state = 0;
        }
    }
}
