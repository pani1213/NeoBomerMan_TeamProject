using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private int _speed;
    private Rigidbody2D _rigidbody;
    
    void Start()
    {
        Player player = GetComponent<Player>();
        _speed = player.PlayerSpeed;

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(h, v);
        dir = dir.normalized;

        _rigidbody.velocity = dir * _speed;
    }
}
