using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    float cooltime = 1;
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
            cooltime = 1;
        }
    }
}
