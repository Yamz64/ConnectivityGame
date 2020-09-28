using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableDoorBehavior : ToggleItemParent
{
    public float speed;
    private bool last_toggle;
    public Vector2 on_position;
    public Vector2 off_position;

    IEnumerator MoveToPosition(float speed, Vector2 start_pos, Vector2 end_pos)
    {
        float current_time = 0;
        while (current_time != 1)
        {
            current_time += speed / 24f;
            yield return new WaitForSeconds(1 / 24f);
            if (current_time > 1.0f) current_time = 1.0f;
            transform.position = Vector2.Lerp(start_pos, end_pos, current_time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (toggled != last_toggle)
        {
            StopAllCoroutines();
            if (toggled)
            {
                StartCoroutine(MoveToPosition(speed, transform.position, on_position));
            }
            else
            {
                StartCoroutine(MoveToPosition(speed, transform.position, off_position));
            }
        }
        last_toggle = toggled;
    }
}
