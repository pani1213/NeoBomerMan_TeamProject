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


    public Direction mydirection;
    public BlockType blockType = BlockType.none;
    public GameObject destroyBrick = null;
    public void InIt( )
    {
        blockType = BlockType.none;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            blockType = BlockType.block;
        }
        if (collision.CompareTag("Brick"))
        {
            destroyBrick = collision.gameObject;
            blockType = BlockType.brick;
        }
    }
}
