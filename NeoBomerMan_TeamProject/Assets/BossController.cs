using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public int bossHp = 1;
    private int bossActionIndex = 0;
    public float bossSpeed = 0.5f;

    public SpriteRenderer mySpriteRenderer;
    public Sprite[] BossAction;
    public BoxCollider2D myBoxCollider;
    public Animation myAnimation;
    public GameObject boomAni, bossRing;

    IEnumerator attackAction;

    WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();

    public List<Vector3> controlPoints; // 베지어 곡선을 정의하는 제어점 리스트
    [Range(0, 1)]
    public float fill;
    private float moveFill, moveTime = 1;
    private bool isMove = true;
    private bool isAttack = true, isHit = true, isPlayerAttack = false;

    private void Update()
    {
        BossMove();
    }
    private void BossMove()
    {
        if (!isMove)
            return;
        fill += Time.deltaTime * bossSpeed;
        if (fill >= 1)
            fill = 0;
        transform.position = CalculateBezierPoint(fill, controlPoints);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttack)
        {
            isMove = false;
            attackAction = AttackMove();
            StartCoroutine(attackAction);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isPlayerAttack)
        {
            Debug.Log(0);
            GameManager.instance.playerMove.PlayerDie();
        }      
    }
    IEnumerator AttackMove()
    {
        isAttack = false;
        mySpriteRenderer.sprite = BossAction[bossActionIndex+1];
        yield return new WaitForSeconds(1);
        isPlayerAttack = true;
        yield return null;
        moveFill = 0;
        moveTime = 0.5f;
        isHit = false;
        while (moveTime >= 0)
        {
            moveTime -= Time.deltaTime;
            moveFill += Time.deltaTime * bossSpeed;
            mySpriteRenderer.transform.localPosition = Vector3.Lerp(mySpriteRenderer.transform.localPosition,
                new Vector3(mySpriteRenderer.transform.localPosition.x, -0.5f), moveFill);
            yield return WaitForEndOfFrame;
        }
        yield return new WaitForSeconds(2);
        isPlayerAttack = false;
        yield return null;
        mySpriteRenderer.sprite = BossAction[bossActionIndex];
        isHit = true;
        moveFill = 0;
        moveTime = 0.5f;
        while (moveTime >= 0)
        {
            moveTime -= Time.deltaTime;
            moveFill += Time.deltaTime * bossSpeed;
            mySpriteRenderer.transform.localPosition = Vector3.Lerp(mySpriteRenderer.transform.localPosition,
                new Vector3(mySpriteRenderer.transform.localPosition.x, 0), moveFill);
            yield return WaitForEndOfFrame;
        }
        yield return new WaitForSeconds(1);
        isMove = true;
        isAttack = true;
    }
    public void BossHitAction()
    {
        if (isHit)
            return;
        SoundManager.instance.PlaySfx(SoundManager.Sfx.robotDie);
        isHit = true;
        bossHp--;
        if (bossHp == 4)
        {
            StartCoroutine(PlayerBoom());
            bossActionIndex = 2;
            mySpriteRenderer.sprite = BossAction[bossActionIndex];
        }
        else if (bossHp == 2)
        { 
            StartCoroutine(PlayerBoom());
            bossActionIndex = 4;
            mySpriteRenderer.sprite = BossAction[bossActionIndex];
        }
        if (bossHp == 0)
        {
            StartCoroutine(PlayerBoom());
            StartCoroutine(playBossAni());
            StopCoroutine(attackAction);
            isMove = false;

        }
        else
            StartCoroutine(HitAction());

    }
    IEnumerator playBossAni()
    {
        bossRing.SetActive(false);
        mySpriteRenderer.sprite = BossAction[6];
        yield return new WaitForSeconds(0.25f);
        mySpriteRenderer.sprite = BossAction[7];
        yield return new WaitForSeconds(0.25f);
        mySpriteRenderer.sprite = BossAction[6];
        yield return new WaitForSeconds(0.25f);
        mySpriteRenderer.sprite = BossAction[7];
        yield return new WaitForSeconds(0.25f);
        mySpriteRenderer.sprite = BossAction[6];
        yield return new WaitForSeconds(1f);
        myAnimation.Play("BossDiedAnimation");
        yield return new WaitForSeconds(2f);
        Scene scene = SceneManager.GetActiveScene();
        int sceneindex = scene.buildIndex;
        SceneManager.LoadScene(++sceneindex);
    }
    IEnumerator PlayerBoom()
    {
        boomAni.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        boomAni.SetActive(false);
    }
    IEnumerator HitAction()
    {
        mySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mySpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }
    Vector3 CalculateBezierPoint(float _value, List<Vector3> points)
    {
        if (points.Count == 1)
            return points[0];

        List<Vector3> reducedPoints = new List<Vector3>();
        for (int i = 0; i < points.Count - 1; i++)
        {
            reducedPoints.Add(Vector3.Lerp(points[i], points[i + 1], _value));
        }
        return CalculateBezierPoint(_value, reducedPoints);
    }
}
