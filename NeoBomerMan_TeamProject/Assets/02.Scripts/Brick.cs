using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject BombPowerItemPrefab;
    public GameObject BombCountItemPrefab;
    public GameObject SpeedItemPrefab;

    public void MakeItem()
    {
        if (Random.Range(0,5) == 0)     // 20%
        {
            int num = Random.Range(0,3);
            GameObject item = null;
            if (num == 0)
            {
                item = Instantiate(BombPowerItemPrefab);
            }
            else if (num == 1)
            {
                item = Instantiate(BombCountItemPrefab);
            }
            else if (num == 2)
            {
                item = Instantiate(SpeedItemPrefab);
            }
            // 위치를 현재 블록 위치로
            item.transform.position = this.transform.position;
        }
    }
}