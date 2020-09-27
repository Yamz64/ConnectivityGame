using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSpawnerBehavior : NetworkBehaviour
{
    public GameObject controlled_object;

    private void Update()
    {
        if (this.isLocalPlayer)
        {
            if (controlled_object != null)
            {
                controlled_object.GetComponent<CharacterMovement>().network_action = Input.GetButtonDown("Action");
                controlled_object.GetComponent<CharacterMovement>().network_horizontal = Input.GetAxis("Horizontal");
                controlled_object.GetComponent<CharacterMovement>().network_vertical = Input.GetAxis("Vertical");
            }
        }
    }
}
