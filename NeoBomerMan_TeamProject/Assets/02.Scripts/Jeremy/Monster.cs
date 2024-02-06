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
    public Animation myAnimation;
    public StageDoor stageDoor;

    private RaycastHit2D hit;
    private RaycastHit2D detection;
    private float RushTime = 1.5f;

    private Direction moveDir;
    private bool isAniPlay = false;

    private void Start()
    {
        isAniPlay = false;
        myAnimation.clip.legacy = true;
        targetPosition = transform.position;
    }
    private void Update()
    {
        myPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);

        if (targetPosition != (Vector2)transform.position && !isAniPlay)
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        else
            Rush();
            SearchDirection();

        if (Input.GetKeyDown(KeyCode.Q))
            MonsterDie();
        
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

    private void Rush()
    {
        moveDistance = 0;
        moveDir = Direction.none;
        switch (Random.Range(0, 4))
        {
            case 0:
                RushDetection(Vector2.up, Direction.up);
                break;
            case 1:
                RushDetection(Vector2.down, Direction.down);
                break;
            case 2:
                RushDetection(Vector2.left, Direction.left);
                break;
            default:
                RushDetection(Vector2.right, Direction.right);
                break;
        }



        // 가로,세로 2유닛씩 Raycast를 사용, Update하고 
        // 만약 레이저 안에 Player의 콜라이더가 감지되면 
        // 1초가 지난 후 Player를 추적하여 속도 *2를 한다
        // 만약 블록 블릭에 부딪혔다면 
        // SearchDirection() 
    }
    private void RushDetection(Vector2 _dir, Direction _direction)
    {
        detection = Physics2D.Raycast(myPosition, _dir, 2f);
        GameObject player = new GameObject("Player");

        if (detection.collider == player)
        {
            RushTime -= Time.deltaTime;
            if (RushTime < 0)
            {
                moveSpeed *= 2;
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
   

    public void MonsterDie()
    {
        
        StartCoroutine(MonsterDieAction());
    }
    IEnumerator MonsterDieAction()
    {
        if (stageDoor != null)
        {
            stageDoor.openScore--;
            if (stageDoor.openScore == 0) stageDoor.DoorOpen();
        }
        GameManager.instance.SetScoer(100);
        isAniPlay = true;
        myAnimation.Play("MonsterDie");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boom"))
        { 
            SearchDirection();
        }
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.playerMove.PlayerDie();
        }
        if (collision.CompareTag("Block"))
        {
            RushTime = 1.5f;
            moveSpeed = 1;
        }
        if (collision.CompareTag("Brick"))
        {
            RushTime = 1.5f;
            moveSpeed = 1;
        }

    }
}
