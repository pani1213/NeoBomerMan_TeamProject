using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private int defaultSpeed = 200;
    public int _speed= 1;
    private Rigidbody2D _rigidbody;
    Vector2 playerDir;
    void Start()
    {
        //Player player = GetComponent<Player>();
        //_speed = player.PlayerSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        playerDir = new Vector2(h, v);
        playerDir = playerDir.normalized;
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = playerDir * defaultSpeed * _speed * Time.deltaTime;
    }
}
