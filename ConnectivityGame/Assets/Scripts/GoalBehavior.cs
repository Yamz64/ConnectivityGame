using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBehavior : MonoBehaviour
{
    public bool bang;

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
            SceneManager.LoadScene(next_scene_name);
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
