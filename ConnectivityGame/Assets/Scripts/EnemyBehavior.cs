using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool direction;                  //direction the enemy is walking false = right, true = left
    public float raycast_distance;
    public float move_speed;
    public Vector2 ground_raycast_spawn;
    public Vector2 forward_raycast_spawn;

    private SpriteRenderer rend;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //handle turning
        //turns when no ground or there is another enemy
        //right
        if (!direction)
        {
            RaycastHit2D ground_hit = Physics2D.Raycast(ground_raycast_spawn + new Vector2(transform.position.x, transform.position.y), Vector2.down, raycast_distance);
            RaycastHit2D enemy_hit = Physics2D.Raycast(forward_raycast_spawn + new Vector2(transform.position.x, transform.position.y), Vector2.right, raycast_distance);
            Debug.DrawRay(ground_raycast_spawn + new Vector2(transform.position.x, transform.position.y), Vector2.down * raycast_distance, Color.red, .5f);
            Debug.DrawRay(forward_raycast_spawn + new Vector2(transform.position.x, transform.position.y), Vector2.right * raycast_distance, Color.blue, .5f);
            if (ground_hit.collider == null)
            {
                direction = !direction;
            }else if (ground_hit.collider.tag != "Ground")
            {
                direction = !direction;
            }
            if (enemy_hit.collider != null)
            {
                if (enemy_hit.collider.tag == "Hazards")
                {
                    direction = !direction;
                }
            }
        }
        //left
        else
        {
            RaycastHit2D ground_hit = Physics2D.Raycast(new Vector2(-ground_raycast_spawn.x, ground_raycast_spawn.y) + new Vector2(transform.position.x, transform.position.y), Vector2.down, raycast_distance);
            RaycastHit2D enemy_hit = Physics2D.Raycast(new Vector2(-forward_raycast_spawn.x, forward_raycast_spawn.y) + new Vector2(transform.position.x, transform.position.y), Vector2.left, raycast_distance);
            Debug.DrawRay(new Vector2(-ground_raycast_spawn.x, ground_raycast_spawn.y) + new Vector2(transform.position.x, transform.position.y), Vector2.down * raycast_distance, Color.red, .5f);
            Debug.DrawRay(new Vector2(-forward_raycast_spawn.x, forward_raycast_spawn.y) + new Vector2(transform.position.x, transform.position.y), Vector2.left * raycast_distance, Color.blue, .5f);
            if (ground_hit.collider == null)
            {
                direction = !direction;
            }
            else if (ground_hit.collider.tag != "Ground")
            {
                direction = !direction;
            }
            if (enemy_hit.collider != null)
            {
                if (enemy_hit.collider.tag == "Hazards")
                {
                    direction = !direction;
                }
            }
        }
        //handle movement
        if (!direction)
        {
            rend.flipX = false;
            rb.velocity = new Vector2(move_speed, rb.velocity.y);
        }
        else
        {
            rend.flipX = true;
            rb.velocity = new Vector2(-move_speed, rb.velocity.y);
        }
    }
}
