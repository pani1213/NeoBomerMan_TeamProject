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

    }
}
