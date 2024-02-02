using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
public class Monster : MonoBehaviour
{
    float moveDistance,moveSpeed = 1;
    public Vector2 myPosition,targetPosition;

    private RaycastHit2D hit;
    private Direction moveDir;

    private void Start()
    {
        targetPosition = transform.position;
    }
    private void Update()
    {
        myPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);

        if (targetPosition != (Vector2)transform.position)
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        else
            SearchDirection();

    }
    private void SearchDirection()
    {
        moveDistance = 0;
        moveDir = Direction.none;

        switch (Random.Range(0, 4))
        {
            case 0:
                SearchProcess(Vector2.up, Direction.up);
                break;
            case 1:
                SearchProcess(Vector2.down, Direction.down);
                break;
            case 2:
                SearchProcess(Vector2.left, Direction.left);
                break;
            default:
                SearchProcess(Vector2.right, Direction.right);
                break;
        }

        SetTargetVector();
    }

    private void SearchProcess(Vector2 _dir,Direction _direction)
    {
        hit = Physics2D.Raycast(myPosition, _dir, 15f);
        if (hit.collider != null)
        {
            float distance = Vector2.Distance(myPosition, hit.collider.transform.position);
            if (distance > moveDistance)
            {
                moveDir = _direction;
                moveDistance = distance;
            }
        }
    }
    private void SetTargetVector()
    {
        moveDistance--;
        if (moveDir == Direction.up)
            targetPosition = new Vector2(transform.position.x, transform.position.y + moveDistance);
        else if (moveDir == Direction.down)
            targetPosition = new Vector2(transform.position.x, transform.position.y + -moveDistance);
        else if (moveDir == Direction.left)
            targetPosition = new Vector2(transform.position.x + -moveDistance, transform.position.y);
        else if (moveDir == Direction.right)
            targetPosition = new Vector2(transform.position.x + moveDistance, transform.position.y);
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("플레이어 충돌");
            Player playerheaith = collision.collider.GetComponent<Player>();
            playerheaith.PlayerHealth -= 1;
        }
        else if(collision.collider.CompareTag("Monster"))
        {
            Debug.Log("몬스터 충돌");
            GameObject Monster1 = collision.collider.GetComponent<GameObject>();
            GameObject Monster2 = collision.collider.GetComponent<GameObject>();
            
            if (Monster1 != null && Monster2 != null)// 오브젝트의 방향이 같지 않다면
            {

            }
            else
            {
                SearchDirection();
            }
        }
        
    }
}
