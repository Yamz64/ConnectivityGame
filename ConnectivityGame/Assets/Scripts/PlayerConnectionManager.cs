using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnectionManager : NetworkBehaviour
{
    public bool called;

    public Object little_one;
    public Object big_one;

    public GameObject spawnObjects(int id, Transform location)
    {
        //Check to see if player object exists
        //check first for little one
        if (id == 0)
        {
            Debug.Log("should be spawning");
            GameObject big_one_ = (GameObject)Instantiate(big_one, location.position, location.rotation);
            NetworkServer.Spawn(big_one_);
            return big_one_;
        }
        //check for big one next
        else
        {
            Debug.Log("should be spawning");
            GameObject little_one_ = (GameObject)Instantiate(little_one, location.position, location.rotation);
            NetworkServer.Spawn(little_one_);
            return little_one_;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkServer.active)
        {
            if (!called)
            {
                if (GameObject.FindGameObjectsWithTag("PlayerSpawner").Length > 1)
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerSpawner");
                    //for the first player pick a random character to assign
                    int temp = Random.Range(0, 2);
                    if (temp == 0)
                    {
                        //big one
                        GameObject big_one_ = spawnObjects(0, players[0].transform);
                        players[0].GetComponent<PlayerSpawnerBehavior>().controlled_object = big_one_;
                    }
                    else
                    {
                        //little one
                        GameObject little_one_ = spawnObjects(1, players[0].transform);
                        players[0].GetComponent<PlayerSpawnerBehavior>().controlled_object = little_one_;
                    }
                    //then assign the opposite character to the other player
                    if (temp == 0)
                    {
                        GameObject little_one_ = spawnObjects(1, players[1].transform);
                        players[1].GetComponent<PlayerSpawnerBehavior>().controlled_object = little_one_;
                    }
                    else
                    {
                        GameObject big_one_ = spawnObjects(0, players[1].transform);
                        players[1].GetComponent<PlayerSpawnerBehavior>().controlled_object = big_one_;
                    }
                }
                called = true;
            }
        }
        else
        {
            Debug.Log("NOT ACTIVE!!!");
        }
    }
}
