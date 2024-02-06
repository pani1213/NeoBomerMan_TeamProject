using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    private int defaultSpeed = 150;
    public int _speed= 1;
    private Rigidbody2D _rigidbody;
    Vector2 playerDir;
    public Animator animator;
    public Animation animation;
    public CircleCollider2D playerCollider;
    Player player;
    void Start()
    {
        animation.clip.legacy = true;
        player = GetComponent<Player>();
        //_speed = player.PlayerSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float h = UnityEngine.Input.GetAxisRaw("Horizontal");
        float v = UnityEngine.Input.GetAxisRaw("Vertical");
        playerDir = new Vector2(h, v);
        playerDir = playerDir.normalized;
        if (GameManager.instance.isInput)
        {
            animator.SetFloat("Horizontal", h);
            animator.SetFloat("Vertical", v);
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.isInput)
            _rigidbody.velocity = playerDir * defaultSpeed * _speed * Time.deltaTime;
    }
    public void PlayerDie()
    {
        player.PlayerHealth--;
        if (player.PlayerHealth <= -1)
            Debug.Log("처음부터");
        animation.Play("PlayerDie");
        GameManager.instance.isInput = false;
        _rigidbody.velocity = Vector2.zero;
        playerCollider.enabled = false;
        StartCoroutine(PlayerRespawn());
    }
    IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(1.2f);
        GameManager.instance.SetLife();
        Debug.Log("살아남");
        GameManager.instance.isInput = true;
        player.gameObject.transform.position = new Vector2(-6f, 4.5f);
        playerCollider.enabled = true;
        player.transform.localScale = Vector2.one;
    }
}
