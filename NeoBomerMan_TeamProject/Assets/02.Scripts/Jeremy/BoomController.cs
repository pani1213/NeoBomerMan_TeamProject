using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum Direction { none, up, down, left, right }
public enum BlockType { none, block,brick }
public class BoomController : MonoBehaviour
{
    public List<BoomEffectController> BoomEffectsUp;
    public List<BoomEffectController> BoomEffectsDown;
    public List<BoomEffectController> BoomEffectsLeft;
    public List<BoomEffectController> BoomEffectsRight;

    public WaitForSeconds boomSecond = new WaitForSeconds(1);
    public Dictionary<Direction,DirValue> fireContainer = new Dictionary<Direction,DirValue>();
    public void Start()
    {
        fireContainer.Add(Direction.up, new DirValue());
        fireContainer.Add(Direction.down, new DirValue());
        fireContainer.Add(Direction.left, new DirValue());
        fireContainer.Add(Direction.right, new DirValue());
        for (int i = 0; i < BoomEffectsUp.Count; i++) BoomEffectsUp[i].InIt(SearchFireRange);
        StartCoroutine(StartBoom());
    }
    public void SearchFireRange()
    {
        Debug.Log("search");
    }

        // 코드 줄이기
    IEnumerator StartBoom()
    {
        yield return new WaitForSeconds(2);
        SetBoomRange();
        for (int i = 0; i < fireContainer[Direction.up].fireRange; i++)
        {
            BoomEffectsUp[i].boomparticle.Play();
        }
        if (fireContainer[Direction.up].blockType == BlockType.brick)
        {
            BoomEffectsUp[fireContainer[Direction.up].fireRange].boomparticle.Play();
            Destroy(BoomEffectsUp[fireContainer[Direction.up].fireRange].destroyBrick);
        }
        for (int i = 0; i < fireContainer[Direction.down].fireRange; i++)
        {
            BoomEffectsDown[i].boomparticle.Play();
        }
        if (fireContainer[Direction.down].blockType == BlockType.brick)
        {
            BoomEffectsDown[fireContainer[Direction.down].fireRange].boomparticle.Play();
            Destroy(BoomEffectsDown[fireContainer[Direction.down].fireRange].destroyBrick);
        }
        for (int i = 0; i < fireContainer[Direction.left].fireRange; i++)
        {
            BoomEffectsLeft[i].boomparticle.Play();
        }
        if (fireContainer[Direction.left].blockType == BlockType.brick)
        {
            BoomEffectsLeft[fireContainer[Direction.left].fireRange].boomparticle.Play();
            Destroy(BoomEffectsLeft[fireContainer[Direction.left].fireRange].destroyBrick);
        }
        for (int i = 0; i < fireContainer[Direction.right].fireRange; i++)
        {
            BoomEffectsRight[i].boomparticle.Play();
        }
        if (fireContainer[Direction.right].blockType == BlockType.brick)
        {
            BoomEffectsRight[fireContainer[Direction.right].fireRange].boomparticle.Play();
            Destroy(BoomEffectsRight[fireContainer[Direction.right].fireRange].destroyBrick);
        }

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    public void SetBoomRange()
    {
        //BoomEffectsUp.Count -> player.fireRange 로 교체
        for (int i = 0; i < BoomEffectsUp.Count; i++)
        {
            if (BoomEffectsUp[i].blockType != BlockType.none)
            {
                fireContainer[Direction.up].blockType = BoomEffectsUp[i].blockType;
                fireContainer[Direction.up].destroyBrick = BoomEffectsUp[i].destroyBrick;
                fireContainer[Direction.up].fireRange = i;
                break;
            }
        }
        for (int i = 0; i < BoomEffectsDown.Count; i++)
        {
            if (BoomEffectsDown[i].blockType != BlockType.none)
            {
                fireContainer[Direction.down].blockType = BoomEffectsDown[i].blockType;
                fireContainer[Direction.down].destroyBrick = BoomEffectsDown[i].destroyBrick;
                fireContainer[Direction.down].fireRange = i;
                break;
            }
        }
        for (int i = 0; i < BoomEffectsLeft.Count; i++)
        {
            if (BoomEffectsLeft[i].blockType != BlockType.none)
            {
                fireContainer[Direction.left].blockType = BoomEffectsLeft[i].blockType;
                fireContainer[Direction.left].destroyBrick = BoomEffectsLeft[i].destroyBrick;
                fireContainer[Direction.left].fireRange = i;
                break;
            }
        }
        for (int i = 0; i < BoomEffectsRight.Count; i++)
        {
            if (BoomEffectsRight[i].blockType != BlockType.none)
            {
                fireContainer[Direction.right].blockType = BoomEffectsRight[i].blockType;
                fireContainer[Direction.right].destroyBrick = BoomEffectsRight[i].destroyBrick;
                fireContainer[Direction.right].fireRange = i;
                break;
            }
        }

    }
}
public class DirValue
{
    public int fireRange= 5;
    public BlockType blockType;
    public GameObject destroyBrick;
}
