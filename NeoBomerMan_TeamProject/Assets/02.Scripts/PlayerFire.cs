using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("폭탄 프리팹")]
    public BoomController BombPrefab;
    public int BombPower = 1;       // 폭탄 레벨
    public int MaxBombCount = 1;
    public int BombCount = 1;    // 최대 설치 가능 폭탄 개수
    public bool GloveItem = false;  // 장갑 아이템 유무 (폭탄 던지기)
    public bool ShoeItem = false;   // 신발 아이템 유무 (폭탄 밀기)
    Vector2 roundedPlayerPosition;
    // 폭탄 오브젝트 풀링
    public int PoolSize = 10;
    public List<BoomController> _bombPool = null;
    private void Awake()
    {
        BombCount = GameManager.instance.playerBoomCount;
        _bombPool = new List<BoomController>();
        for (int i = 0; i < PoolSize; i++)
        {
            BoomController bomb = Instantiate(BombPrefab);
            bomb.gameObject.SetActive(false);
            _bombPool.Add(bomb);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && BombCount > 0)
        {
            BombCount--;
            // 플레이어 위치 기준  x: 반올림   y: 내림 + 0.5
            roundedPlayerPosition = new Vector2(Mathf.Round(transform.position.x), Mathf.Floor(transform.position.y) + 0.5f);
            BoomController bomb = null;
            foreach (BoomController b in _bombPool)
            {
                if (b.gameObject.activeInHierarchy == false)
                {
                    bomb = b;
                    break;
                }
            }
            //GameManager.instance.boomControllers.Add(bomb);
            bomb.transform.position = roundedPlayerPosition;
            bomb.gameObject.SetActive(true);
            bomb.InIt();
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Boom"))
    //    {
    //        other.GetComponent<Collider2D>().isTrigger = false;
    //    }
    //}
    //
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (GloveItem && collision.collider.CompareTag("Boom"))
    //    {
    //        ContactPoint2D contactPoint = collision.contacts[0];
    //        Vector2 collisionDirection = -contactPoint.normal;
    //        if (Mathf.Abs(collisionDirection.x) > Mathf.Abs(collisionDirection.y))
    //        {
    //            collisionDirection.y = 0;
    //        }
    //        else
    //        {
    //            collisionDirection.x = 0;
    //        }
    //
    //        Vector2 newPosition = (Vector2)collision.gameObject.transform.position + collisionDirection * 2f;
    //        collision.gameObject.transform.position = newPosition;
    //        collision.collider.isTrigger = true;
    //        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
    //        if (rb != null)
    //        {
    //            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    //            rb.velocity = Vector2.zero;
    //        }
    //    } 
    //    
    //
    //}
}
