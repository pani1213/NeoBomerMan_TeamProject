using CartoonFX; // CartoonFX에 대한 네임스페이스가 필요한 경우 사용하세요.
using System.Collections;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public enum WallMonsterType
{
    Rush,
    Boom
}
public class Monster_02 : MonoBehaviour
{
    public int MonsterHealth = 1;
    float moveDistance, moveSpeed = 1;
    public Vector2 myPosition, targetPosition;

    private RaycastHit2D hit;
    private Direction moveDir;

    Vector2 startPosition;

    bool isCooldown = false; // 쿨타임 여부를 나타내는 변수
    float cooldownTimer = 0f;
    public WallMonsterType WallMonsterType;

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

        if(MonsterHealth == 0)
        {
            Kill();
        }
        // 플레이어를 찾아서 추적
        FindPlayer();
    }

    private void SearchDirection()
    {
        startPosition = transform.position;
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

    private void Rush()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(myPosition, 2f, LayerMask.GetMask("Player"));

        if (playerCollider != null)
        {  
            GameObject playerObject = playerCollider.gameObject;

            if (!isCooldown)
            {

                moveSpeed *= 2;

                isCooldown = true;

                cooldownTimer = 5f;
            }
            else
            {
                // 쿨타임 중에는 다른 동작 수행하지 않음
            }

            if (WallCollisionDetected())
            {
                SearchDirection(); // 원상복귀

                isCooldown = false;
            }
        }

    }

        bool WallCollisionDetected()
        {
            // Raycast로 충돌 여부 검사
            RaycastHit2D wallHit = Physics2D.Raycast(myPosition, targetPosition - myPosition, Mathf.Infinity, LayerMask.GetMask("Block", "Brick"));

            return wallHit.collider != null && wallHit.distance <= 0f;

        }

    public void WallMonster()
    {
        //벽안 몬스터는 목숨이 2개 
        //몬스터는 벽 안에 있어 벽이 파괴되면 목숨이 1개 줄어든다
        //벽이 파괴되면 몬스터는 enem에 있는 Rush, Boom 둘 중 하나가 랜덤으로 생성된다
        //벽안의 몬스터는 spped가 -0.5가 된다
    }

    private void SearchProcess(Vector2 _dir, Direction _direction)
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
            targetPosition = new Vector2(transform.position.x, transform.position.y - moveDistance);
        else if (moveDir == Direction.left)
            targetPosition = new Vector2(transform.position.x + -moveDistance, transform.position.y);
        else if (moveDir == Direction.right)
            targetPosition = new Vector2(transform.position.x + moveDistance, transform.position.y);
    }

    public void FindPlayer()
    {
        // 플레이어를 2 유닛 반경으로 감지
        Collider2D playerCollider = Physics2D.OverlapCircle(myPosition, 2f, LayerMask.GetMask("Player"));

        if (playerCollider != null)
        {

            GameObject playerObject = playerCollider.gameObject;

            Vector3 playerPosition = playerObject.transform.position;
        }
    }
    public void Kill()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("플레이어 충돌");
            Player playerHealth = collision.collider.GetComponent<Player>();
            playerHealth.PlayerHealth -= 1;
        }
        else if (collision.collider.CompareTag("Monster"))
        {
            Debug.Log("몬스터 충돌");
            targetPosition = startPosition;
        }
    }
}
