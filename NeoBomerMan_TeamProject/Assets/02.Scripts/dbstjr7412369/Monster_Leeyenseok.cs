
using UnityEngine;


public class Monster_Leeyenseok : MonoBehaviour
{
    // 몬스터 체력: 체력 1 벽 몬스터 체력: 2/ 0으로 될 시 사망
    public float Monster_Health = 1f;
    public float WallMonster_Health = 2f;

    //좀비몬스터(돌진), 로봇몬스터(폭발)
    public GameObject BoomRobotPrefab;
    public GameObject RushZombiePrefab;

    //벽 안 몬스터 좀비몬스터(돌진), 로봇몬스터(폭발)
    public GameObject WallRobotPrefab;
    public GameObject WallZombiePrefab;

    // UFO 4개
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
            // 플레이어 스크립트를 가져온다.
           // Player player = collision.collider.GetComponent<Player>();
            //Player player = collision.collider.GetComponent();
            // 플레이어 체력을 -= 1
           // player.DecreaseHealth(1);


            Death();
        }
    }
    private void Death()
    {
        Destroy(this);
    }
}
