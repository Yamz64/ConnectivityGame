using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBrothersBehavior : MonoBehaviour
{
    public bool active;
    private BigOneBehavior big_one;
    private LittleOneBehavior little_one;

    // Start is called before the first frame update
    void Start()
    {
        big_one = transform.GetChild(0).GetComponent<BigOneBehavior>();
        little_one = transform.GetChild(1).GetComponent<LittleOneBehavior>();

        big_one.active = false;
        little_one.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) active = !active;
        if (active)
        {
            big_one.active = true;
            little_one.active = false;
        }
        else
        {
            big_one.active = false;
            little_one.active = true;
        }
    }
}
