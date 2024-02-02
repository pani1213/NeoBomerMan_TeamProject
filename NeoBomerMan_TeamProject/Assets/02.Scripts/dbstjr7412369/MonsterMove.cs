using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed = 100f;
    private Vector2 dir = new Vector2(0, 1);
    private Rigidbody2D myRigid;
    private Vector2 previousPosition; 
    private float idleTimer = 0f;

    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {

        if (idleTimer >= 1f)
        {
            Move();
            idleTimer = 0f;
        }
        previousPosition = transform.position;
       
    }
    void FixedUpdate()
    {
        myRigid.velocity = dir * speed * Time.deltaTime;

    }
    public void Move()
    {
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                dir = Vector2.up;
                break;
            case 1:
                dir = Vector2.down;
                break;
            case 2:
                dir = Vector2.left;
                break;
            default:
                dir = Vector2.right;
                break;
        }
        
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Block") || collision.collider.CompareTag("Brick"))
        {
            Debug.Log("벽 충돌");
            Move();
        }
        else if (collision.collider.CompareTag("Monster"))
        {
            Monster monster = collision.collider.GetComponent<Monster>();
            Debug.Log("몬스터 충돌");
            Move();
        }
        else if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("플레이어 충돌");
            // 플레이어와의 충돌 처리
        }
    }

  
}



