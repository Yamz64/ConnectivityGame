using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOneBehavior : CharacterMovement
{
    private Vector3 held_object_transform;      //position of held object starting the game
    private GameObject held_object_position;    //current position of held object

    new void Start()
    {
        base.Start();
        held_object_position = transform.GetChild(0).gameObject;
        held_object_transform = held_object_position.transform.localPosition;
    }

    new void Update()
    {
        base.Update();
        //handle flipping the held object
        if (flipped)
        {
            held_object_position.transform.localPosition = new Vector3(-held_object_transform.x, held_object_transform.y, held_object_transform.z);
        }
        else
        {
            held_object_position.transform.localPosition = new Vector3(held_object_transform.x, held_object_transform.y, held_object_transform.z);
        }
    }
}
