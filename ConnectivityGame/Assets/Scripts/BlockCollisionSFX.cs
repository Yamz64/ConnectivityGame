using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollisionSFX : MonoBehaviour
{
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Big One" || other.gameObject.tag != "Little One" || other.gameObject.tag != "Enemy")
        AudioSource.PlayClipAtPoint(source.clip, transform.position);
    }
}
