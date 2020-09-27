using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSpawnerBehavior : NetworkBehaviour
{
    bool type;                  //type of character spawned Big = false, little = true

    public Object big_one;
    public Object little_one;

    private GameObject controlled_object;

    // Start is called before the first frame update
    public override void OnStartServer()
    {
        spawnObjects();
    }

    public void spawnObjects()
    {
        //Check to see if player object exists
        //check first for little one
        if (GameObject.FindGameObjectWithTag("Little One") != null)
        {
            Debug.Log("should be spawning");
            GameObject big_one_ = (GameObject)Instantiate(big_one, transform.position, transform.rotation);
            NetworkServer.Spawn(big_one_);
            type = false;
        }
        //check for big one next
        else if (GameObject.FindGameObjectWithTag("Big One") != null)
        {
            Debug.Log("should be spawning");
            GameObject little_one_ = (GameObject)Instantiate(little_one, transform.position, transform.rotation);
            NetworkServer.Spawn(little_one_);
            type = true;
        }
        //if no player exists pick a random one
        else
        {
            int temp = Random.Range(0, 2);
            if (temp == 0)
            {
                GameObject big_one_ = (GameObject)Instantiate(big_one, transform.position, transform.rotation);
                NetworkServer.Spawn(big_one_);
                type = false;
            }
            else
            {
                GameObject little_one_ = (GameObject)Instantiate(little_one, transform.position, transform.rotation);
                NetworkServer.Spawn(little_one_);
                type = true;
            }
        }
    }

    private void Update()
    {
        if (this.isLocalPlayer)
        {
            if (!type)
            {
                controlled_object = GameObject.FindGameObjectWithTag("Big One");
            }
            else
            {
                controlled_object = GameObject.FindGameObjectWithTag("Little One");
            }

            controlled_object.GetComponent<CharacterMovement>().network_action = Input.GetButtonDown("Action");
            controlled_object.GetComponent<CharacterMovement>().network_horizontal = Input.GetAxis("Horizontal");
            controlled_object.GetComponent<CharacterMovement>().network_vertical = Input.GetAxis("Vertical");
        }
    }
}
