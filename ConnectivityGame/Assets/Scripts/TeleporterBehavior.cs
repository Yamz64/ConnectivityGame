using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBehavior : MonoBehaviour
{
    public Color color;

    public GameObject last_teleported_object;

    private GameObject teleporter_a;
    private GameObject teleporter_b;

    // Start is called before the first frame update
    void Start()
    {
        teleporter_a = transform.GetChild(0).gameObject;
        teleporter_b = transform.GetChild(1).gameObject;
        teleporter_a.GetComponent<SpriteRenderer>().color = color;
        teleporter_b.GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        TeleporterHelper a = teleporter_a.GetComponent<TeleporterHelper>();
        TeleporterHelper b = teleporter_b.GetComponent<TeleporterHelper>();

        //entered teleporter a
        if (a.teleported_object != null)
        {
            if (last_teleported_object == null)
            {
                if (a.teleported_object.tag == "Little One" || a.teleported_object.tag == "Big One")
                {
                    if (Input.GetButtonDown("Action"))
                    {
                        a.teleported_object.transform.position = teleporter_b.transform.position;
                        last_teleported_object = a.teleported_object;
                    }
                }
                else
                {
                    a.teleported_object.transform.position = teleporter_b.transform.position;
                    last_teleported_object = a.teleported_object;
                }
            }
        }
        else if (b.teleported_object != null)
        {
            if (last_teleported_object == null)
            {
                if (b.teleported_object.tag == "Little One" || b.teleported_object.tag == "Big One")
                {
                    if (Input.GetButtonDown("Action"))
                    {
                        b.teleported_object.transform.position = teleporter_a.transform.position;
                        last_teleported_object = b.teleported_object;
                    }
                }
                else
                {
                    b.teleported_object.transform.position = teleporter_a.transform.position;
                    last_teleported_object = b.teleported_object;
                }
            }
        }
        if(a.teleported_object == null && b.teleported_object == null)
        {
            last_teleported_object = null;
        }
    }
}
