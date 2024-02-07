using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    float cooltime = 3;
    float rotatez = 720, rotate_y = 0;
    public GameObject mainTitle,textobj;
    public SpriteRenderer circleRander;

    bool isPlayAni = false;
    bool isPlayRotate = false;

    float colorA = 255f;
    void Start()
    {
        StartCoroutine(TitleAnimationCoroutin());
    }
    IEnumerator TitleAnimationCoroutin()
    {
        yield return new WaitForSeconds(3f);
        while (colorA >= 0)
        {
            colorA -= Time.deltaTime * 100;
            circleRander.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, colorA / 255f);
            yield return new WaitForEndOfFrame();
        }
        SoundManager.instance.PlayBgm(SoundManager.Bgm.Title);
        mainTitle.SetActive(true);
        textobj.SetActive(true);
        mainTitle.transform.DOScale(90f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);
        isPlayRotate = true;
        yield return new WaitForSeconds(1);
        isPlayAni = true;
    }
    void Update()
    {
        if (Input.anyKeyDown)// && isPlayAni)
        { 
            GameManager.instance.ButtonActionNextScene();
        }

        if (isPlayRotate)
            cooltime -= Time.deltaTime;

       if (cooltime <= 0)
       {
           cooltime = 3;
            mainTitle.transform.DORotate(new Vector3(0, rotate_y, rotatez), 1, RotateMode.FastBeyond360).SetEase(Ease.InCubic);
           if (rotatez == 720)
           {
               rotatez = 0;
               rotate_y = 720;
           }
           else
           {
               rotatez = 720;
               rotate_y = 0;
           }
       }
    }
}
