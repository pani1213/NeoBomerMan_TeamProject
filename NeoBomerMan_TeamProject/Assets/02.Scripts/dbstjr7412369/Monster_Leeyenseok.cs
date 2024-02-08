
using UnityEngine;


public class Monster_Leeyenseok : MonoBehaviour
{
    // ���� ü��: ü�� 1 �� ���� ü��: 2/ 0���� �� �� ���
    public float Monster_Health = 1f;
    public float WallMonster_Health = 2f;

    //�������(����), �κ�����(����)
    public GameObject BoomRobotPrefab;
    public GameObject RushZombiePrefab;

    //�� �� ���� �������(����), �κ�����(����)
    public GameObject WallRobotPrefab;
    public GameObject WallZombiePrefab;

    // UFO 4��
    public GameObject shipBeige_mannedPrefab;
    public GameObject shipBlue_mannedPrefab;
    public GameObject shipGreen_mannedPrefab;
    public GameObject shipPink_mannedPrefab;


    private Vector2 _dir;

    private GameObject _target;

    

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // �÷��̾� ��ũ��Ʈ�� �����´�.
           // Player player = collision.collider.GetComponent<Player>();
            //Player player = collision.collider.GetComponent();
            // �÷��̾� ü���� -= 1
           // player.DecreaseHealth(1);


            Death();
        }
    }
    private void Death()
    {
        Destroy(this);
    }
}
