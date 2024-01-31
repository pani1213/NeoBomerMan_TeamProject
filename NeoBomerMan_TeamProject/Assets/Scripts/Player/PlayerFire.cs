using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("폭탄 프리팹")]
    public GameObject BombPrefab;

    public int BombPower = 1;       // 폭탄 레벨
    public int MaxBombCount = 1;    // 최대 설치 가능 폭탄 개수

   

    // 폭탄 오브젝트 풀링
    public int PoolSize = 10;
    public List<GameObject> _bombPool = null;

    private void Awake()
    {
        

        _bombPool = new List<GameObject>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bomb = Instantiate(BombPrefab);
            bomb.SetActive(false);

            _bombPool.Add(bomb);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 플레이어 위치 기준  x: 반올림   y: 내림 + 0.5
            Vector2 playerPosition = this.transform.position;
            Vector2 roundedPlayerPosition = new Vector2(Mathf.Round(playerPosition.x), Mathf.Floor(playerPosition.y) + 0.5f);

            GameObject bomb = null;
            foreach (GameObject b in _bombPool)
            {
                if (b.activeInHierarchy == false)
                {
                    bomb = b;
                    break;
                }
            }
            bomb.transform.position = roundedPlayerPosition;
            bomb.gameObject.SetActive(true);

            
        }

       
    }

}
