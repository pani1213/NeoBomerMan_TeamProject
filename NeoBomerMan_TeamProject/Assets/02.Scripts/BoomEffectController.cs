using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomEffectController : MonoBehaviour
{
    public BoomController boomController;
    public ParticleSystem boomparticle;
    public BoxCollider2D myCollider;

    Action action = null;

    public int boomIndex;
    public Direction mydirection;
    public BlockType blockType = BlockType.none;
    public GameObject destroyBrick = null;
    public void InIt(Action _action)
    {
        if (action == null)
            action = _action;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            if (action != null)
                action();
            blockType = BlockType.block;
        }
        if (collision.CompareTag("Brick"))
        {
            if (action != null)
                action();
            destroyBrick = collision.gameObject;
            blockType = BlockType.brick;
        }
    }
}
