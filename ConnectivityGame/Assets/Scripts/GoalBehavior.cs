using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GoalBehavior : MonoBehaviour
{
    public bool bang;
    public bool completed;

    public string drawing_name;
    public string next_scene_name;

    private bool little_one;
    private bool big_one;

    private AudioSource victory_theme;

    IEnumerator Victory_Sequence()
    {
        victory_theme.Play();
        yield return new WaitForSeconds(victory_theme.clip.length);
        if (next_scene_name != "")
        {
            GameObject little_one = GameObject.FindGameObjectWithTag("Little One");
            GameObject big_one = GameObject.FindGameObjectWithTag("Big One");
            little_one.GetComponent<CharacterMovement>().cam_mode = true;
            big_one.GetComponent<CharacterMovement>().cam_mode = true;
            little_one.GetComponent<DrawLine>().clampbounds = Vector2.positiveInfinity;
            little_one.GetComponent<DrawLine>().draw_cursor.transform.localScale = new Vector3(5, 5, 5);
            big_one.GetComponent<DrawLine>().infinite_draw = true;
            little_one.GetComponent<DrawLine>().infinite_draw = true;
            GameObject all_tiles = GameObject.FindGameObjectWithTag("Ground");
            GameObject hazards = GameObject.FindGameObjectWithTag("Hazards");
            GameObject conveyor = GameObject.FindGameObjectWithTag("Conveyor");

            if(all_tiles != null)
            all_tiles.GetComponent<Tilemap>().color = new Color(1, 1, 1, 0);
            if(all_tiles != null)
            hazards.GetComponent<Tilemap>().color = new Color(1, 1, 1, 0);
            completed = true;
        }
        else
        {
            Debug.Log("No scene name");
        }
    }

    private void Start()
    {
        victory_theme = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(little_one && big_one)
        {
            if (!bang)
            {
                StartCoroutine(Victory_Sequence());
                bang = true;
            }
        }
        if (completed)
        {
            if (Input.GetButtonDown("Action"))
            {
                ScreenCapture.CaptureScreenshot(Application.dataPath + "/Resources/Drawings" + drawing_name);
                SceneManager.LoadScene(next_scene_name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Little One") little_one = true;
        if (other.tag == "Big One") big_one = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Little One") little_one = false;
        if (other.tag == "Big One") big_one = false;
    }
}
