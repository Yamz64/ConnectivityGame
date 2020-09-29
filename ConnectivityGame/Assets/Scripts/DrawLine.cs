using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawLine : MonoBehaviour
{
    public float chalk_amount;
    public float chalk_loss;

    private float max_chalk;

    public Object line_prefab;

    public AudioSource sound;

    private LineRenderer line_renderer;

    public List<Vector2> mouse_positions;

    public Camera drawing_camera;

    private List<GameObject> lines;

    private void Start()
    {
        lines = new List<GameObject>();
        max_chalk = chalk_amount;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!sound.isPlaying && chalk_amount > 0.0f)
                    sound.Play();
                CreateLine();
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 temp_mouse_pos = drawing_camera.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(temp_mouse_pos, mouse_positions[mouse_positions.Count - 1]) > .1f)
                {
                    if(chalk_amount > 0.0f)
                    UpdateLine(temp_mouse_pos);
                }
            }
            if (chalk_amount < 0.0f) chalk_amount = 0.0f;

            //clear all lines and restore chalk
            if (Input.GetMouseButtonDown(1))
            {
                chalk_amount = max_chalk;
                for(int i=0; i<lines.Count; i++)
                {
                    Destroy(lines[i].gameObject);
                }
                lines.Clear();
            }

            if (Input.GetMouseButtonUp(0) || chalk_amount <= 0.0f)
            {
                sound.Stop();
            }
        }
    }

    void CreateLine()
    {
        GameObject current_line = (GameObject)Instantiate(line_prefab, Vector3.zero, Quaternion.identity);
        line_renderer = current_line.GetComponent<LineRenderer>();
        mouse_positions.Clear();
        mouse_positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouse_positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.up * .1f);
        line_renderer.SetPosition(0, mouse_positions[0]);
        line_renderer.SetPosition(1, mouse_positions[1]);
        lines.Add(current_line);
        //NetworkServer.Spawn(current_line);
    }

    void UpdateLine(Vector2 new_pos)
    {
        mouse_positions.Add(new_pos);
        line_renderer.positionCount++;
        line_renderer.SetPosition(line_renderer.positionCount - 1, new_pos);
        chalk_amount -= chalk_loss;
    }
}
