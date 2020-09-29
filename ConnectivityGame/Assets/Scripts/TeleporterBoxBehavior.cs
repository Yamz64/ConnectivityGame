using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBoxBehavior : MonoBehaviour
{
    public float delay;
    public GameObject teleported_Object;

    IEnumerator Teleport_Routine()
    {
        yield return new WaitForSeconds(delay);
        teleported_Object.transform.position = transform.position;
        Destroy(gameObject);
    }

    public void Teleport()
    {
        StartCoroutine(Teleport_Routine());
    }
}
