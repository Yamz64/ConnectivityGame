using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherBehavior : ButtonParent
{
    public float delay;
    public float launch_speed;
    public Object Arrow;

    private float max_delay;

    private void Start()
    {
        max_delay = delay;
        delay = 0.0f;
    }

    private void Update()
    {
        if(delay > 0.0f)
        {
            delay -= 1.0f * Time.deltaTime;
        }
    }

    public override void Activate()
    {
        if (delay <= 0.0f)
        {
            GameObject arrow = (GameObject)Instantiate(Arrow, transform.position, transform.rotation);
            arrow.GetComponent<Rigidbody2D>().velocity = transform.right * launch_speed;
            arrow.GetComponent<ArrowBehavior>().Die();
            delay = max_delay;
        }
    }
}
