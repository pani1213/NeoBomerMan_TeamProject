using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    float cooltime = 3;
    float rotatez = 720, rotate_y = 0;
    void Start()
    {
        transform.DOScale(2.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);
    }

    // Update is called once per frame
    void Update()
    {
        cooltime -= Time.deltaTime;
        if (cooltime <= 0)
        {
            cooltime = 3;
            transform.DORotate(new Vector3(0, rotate_y, rotatez), 1, RotateMode.FastBeyond360).SetEase(Ease.InCubic);
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
