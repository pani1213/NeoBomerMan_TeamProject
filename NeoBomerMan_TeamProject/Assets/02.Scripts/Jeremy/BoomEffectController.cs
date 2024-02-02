using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomEffectController : MonoBehaviour
{
    //public BoomController boomController;
    public ParticleSystem boomparticle;
    public BoxCollider2D myCollider;

    public Direction mydirection;
    public BlockType blockType = BlockType.none;
    public GameObject destroyBrick = null;
    public Action action;

    public bool isfire = false;
    public void InIt(Action _action = null)
    {
        myCollider.enabled = true;
        isfire = false;
        blockType = BlockType.none;
        if(_action != null)
            action = _action;
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (action != null)
        {
            action();
            action = null;
        }
        if (collision.CompareTag("Block"))
        {
            blockType = BlockType.block;
        }
        if (collision.CompareTag("Brick"))
        {
            destroyBrick = collision.gameObject;
            blockType = BlockType.brick;
        }
        if (collision.CompareTag("Boom")&& isfire)
        {
            BoomController bom = collision.GetComponent<BoomController>();
            if (bom != null)
            {
                bom.StopAllCoroutines();
                bom.StartCoroutine(bom.StartBoom(true));
            }
        }
        if (collision.CompareTag("Player") && isfire)
        {
            Debug.Log("Player");
            collision.GetComponent<PlayerMove>().PlayerDie();
        }
    }
}
