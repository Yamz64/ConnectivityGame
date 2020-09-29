using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserBehavior : ButtonParent
{
    public bool toggle;

    public enum SpawnMode { BLOCK, BREAKABLEBLOCK, JUMPBLOCK };
    public SpawnMode mode;

    public Vector2 offset;
    public List<Object> spawnables;                                 //Index 0 = bloc, Index 1 = breakableblock, Index 2 = JumpBlock

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public override void Activate()
    {
        switch (mode) {
            case SpawnMode.BLOCK:
                anim.SetInteger("SpawnState", 0);
                Instantiate(spawnables[0], (Vector2)transform.position + offset, transform.rotation);
                break;
            case SpawnMode.BREAKABLEBLOCK:
                anim.SetInteger("SpawnState", 1);
                Instantiate(spawnables[1], (Vector2)transform.position + offset, transform.rotation);
                break;
            case SpawnMode.JUMPBLOCK:
                anim.SetInteger("SpawnState", 2);
                Instantiate(spawnables[2], (Vector2)transform.position + offset, transform.rotation);
                break;
            default:
                break;
        }
    }
}
