using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerHealth = 2;    // 플레이어 생명 2개
    public int PlayerSpeed = 2;     // 플레이어 속도

    

    void Start()
    {
        transform.position = this.transform.position;     // 초기위치 설정
    }
}
