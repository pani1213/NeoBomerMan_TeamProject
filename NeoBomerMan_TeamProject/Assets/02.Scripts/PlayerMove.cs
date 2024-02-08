using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    private int defaultSpeed = 150;
    public float _speed= 1;
    private Rigidbody2D _rigidbody;
    Vector2 playerDir;
    public Animator animator;
    public Animation myAnimation;
    public CircleCollider2D playerCollider;
    Player player;

    bool isDie = false;
    void Start()
    {
        myAnimation.clip.legacy = true;
        player = GetComponent<Player>();
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
        if (isDie)
            return;
        SoundManager.instance.PlaySfx(SoundManager.Sfx.playerDie);
        isDie = true;
        player.PlayerHealth--;
        if (player.PlayerHealth <= -1)
        {
            GameManager.instance.canvasController.gameOver.SetActive(true);
            GameManager.instance.isInput = false;
            StartCoroutine(OneSecond());
        }
        myAnimation.Play("PlayerDie");
        GameManager.instance.isInput = false;
        if (GameManager.instance.isHurry)
        { 
            SoundManager.instance.PlayBgm(SoundManager.Bgm.ingame);
            GameManager.instance.isHurry = false;
        }
        GameManager.instance.StartTimer();
        _rigidbody.velocity = Vector2.zero;
        playerCollider.enabled = false;
        if(player.PlayerHealth >= 0)
        StartCoroutine(PlayerRespawn());
    }
    IEnumerator OneSecond()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.isGameOver = true;
    }
    IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(1.2f);
        GameManager.instance.SetLife();
        GameManager.instance.isInput = true;
        player.gameObject.transform.position = new Vector2(-6f, 4.5f);
        playerCollider.enabled = true;
        player.transform.localScale = Vector2.one;
        isDie = false;
    }
}
