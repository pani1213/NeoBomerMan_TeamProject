using CartoonFX; // CartoonFX�� ���� ���ӽ����̽��� �ʿ��� ��� ����ϼ���.
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

    bool isCooldown = false; // ��Ÿ�� ���θ� ��Ÿ���� ����
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
        // �÷��̾ ã�Ƽ� ����
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
                // ��Ÿ�� �߿��� �ٸ� ���� �������� ����
            }

            if (WallCollisionDetected())
            {
                SearchDirection(); // ���󺹱�

                isCooldown = false;
            }
        }

    }

        bool WallCollisionDetected()
        {
            // Raycast�� �浹 ���� �˻�
            RaycastHit2D wallHit = Physics2D.Raycast(myPosition, targetPosition - myPosition, Mathf.Infinity, LayerMask.GetMask("Block", "Brick"));

            return wallHit.collider != null && wallHit.distance <= 0f;

        }

    public void WallMonster()
    {
        //���� ���ʹ� ����� 2�� 
        //���ʹ� �� �ȿ� �־� ���� �ı��Ǹ� ����� 1�� �پ���
        //���� �ı��Ǹ� ���ʹ� enem�� �ִ� Rush, Boom �� �� �ϳ��� �������� �����ȴ�
        //������ ���ʹ� spped�� -0.5�� �ȴ�
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
        // �÷��̾ 2 ���� �ݰ����� ����
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
            Debug.Log("�÷��̾� �浹");
            Player playerHealth = collision.collider.GetComponent<Player>();
            playerHealth.PlayerHealth -= 1;
        }
        else if (collision.collider.CompareTag("Monster"))
        {
            Debug.Log("���� �浹");
            targetPosition = startPosition;
        }
    }
}
