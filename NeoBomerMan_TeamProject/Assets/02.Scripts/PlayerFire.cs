using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    RaycastHit2D hit;
    public LayerMask LayerMask;
    bool isGround = false;
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
    void FixedUpdate()
    {

        hit = Physics2D.Raycast(transform.position, Vector2.zero, LayerMask);
        Debug.DrawRay(transform.position, Vector2.zero, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Ground"))
                isGround = true;
            else
                isGround = false;
        }
    }
    void Update()
    {

        if (isGround && Input.GetKeyDown(KeyCode.Space) && BombCount > 0 && GameManager.instance.isInput)
        {
            BombCount--;
            BoomController bomb = null;
            foreach (BoomController b in _bombPool)
            {
                if (b.gameObject.activeInHierarchy == false)
                {
                    bomb = b;
                    break;
                }
            }
            bomb.transform.position = hit.collider.transform.position;
            bomb.gameObject.SetActive(true);
            bomb.InIt();
        }

      
    }
   
}
