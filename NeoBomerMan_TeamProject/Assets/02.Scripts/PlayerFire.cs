using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("폭탄 프리팹")]
    public BoomController BombPrefab;
    public int BombPower = 1;       // 폭탄 레벨
    public int MaxBombCount = 1;    // 최대 설치 가능 폭탄 개수
    Vector2 roundedPlayerPosition;
    // 폭탄 오브젝트 풀링
    public int PoolSize = 10;
    public List<BoomController> _bombPool = null;
    private void Awake()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && MaxBombCount > 0)
        {
            MaxBombCount--;
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
}
