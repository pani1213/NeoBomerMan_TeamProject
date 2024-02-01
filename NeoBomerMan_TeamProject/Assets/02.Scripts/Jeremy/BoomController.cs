using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public enum Direction { none, up, down, left, right }
public enum BlockType { none, block,brick }
public class BoomController : MonoBehaviour
{
    public List<BoomEffectController> BoomEffectsUp;
    public List<BoomEffectController> BoomEffectsDown;
    public List<BoomEffectController> BoomEffectsLeft;
    public List<BoomEffectController> BoomEffectsRight;

    public GameObject BombPowerItemPrefab;
    public GameObject BombCountItemPrefab;
    public GameObject SpeedItemPrefab;

    public WaitForSeconds boomSecond = new WaitForSeconds(1);
    public Dictionary<Direction, DirValue> fireContainer;//= new Dictionary<Direction,DirValue>();
    public void InIt()
    {
        fireContainer = new Dictionary<Direction, DirValue>();
        fireContainer.Add(Direction.up, new DirValue());
        fireContainer.Add(Direction.down, new DirValue());
        fireContainer.Add(Direction.left, new DirValue());
        fireContainer.Add(Direction.right, new DirValue());
        for (int i = 0; i < BoomEffectsUp.Count; i++) BoomEffectsUp[i].InIt();
        for (int i = 0; i < BoomEffectsDown.Count; i++) BoomEffectsDown[i].InIt();
        for (int i = 0; i < BoomEffectsLeft.Count; i++) BoomEffectsLeft[i].InIt();
        for (int i = 0; i < BoomEffectsRight.Count; i++) BoomEffectsRight[i].InIt();
        StartCoroutine(StartBoom());
    }

    public IEnumerator StartBoom()
    {
        yield return new WaitForSeconds(2);
        SetBoomRange();
        BoomDirectionProcess(Direction.up, BoomEffectsUp);
        BoomDirectionProcess(Direction.down, BoomEffectsDown);
        BoomDirectionProcess(Direction.left, BoomEffectsLeft);
        BoomDirectionProcess(Direction.right, BoomEffectsRight);
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        GameManager.instance.playerFire.MaxBombCount++;
    }
    private void BoomDirectionProcess(Direction _direction,List<BoomEffectController> _boomEffecter)
    {
        Debug.Log($"{_direction},{fireContainer[_direction].fireRange}");
        for (int i = 0; i < fireContainer[_direction].fireRange; i++)
        {
            _boomEffecter[i].boomparticle.Play();
        }
        if (fireContainer[_direction].blockType == BlockType.brick)
        {
            _boomEffecter[fireContainer[_direction].fireRange].boomparticle.Play();
            MakeItem(_boomEffecter[fireContainer[_direction].fireRange].destroyBrick);
            Destroy(_boomEffecter[fireContainer[_direction].fireRange].destroyBrick);       
        }
    }
    private void SetBoomRange()
    {
        //BoomEffectsUp.Count -> player.fireRange ·Î ±³Ã¼
        BoomRangeProcess(Direction.up, BoomEffectsUp);
        BoomRangeProcess(Direction.down, BoomEffectsDown);
        BoomRangeProcess(Direction.left, BoomEffectsLeft);
        BoomRangeProcess(Direction.right, BoomEffectsRight);
    }
    private void BoomRangeProcess(Direction _direction,List<BoomEffectController> _boomEffecter)
    {
        fireContainer[_direction].fireRange = GameManager.instance.playerFire.BombPower;

        for (int i = 0; i < fireContainer[_direction].fireRange; i++)
        {
            if (_boomEffecter[i].blockType != BlockType.none)
            {
                fireContainer[_direction].blockType = _boomEffecter[i].blockType;
                fireContainer[_direction].destroyBrick = _boomEffecter[i].destroyBrick;
                fireContainer[_direction].fireRange = i;
                break;
            }
        }
    }
    public void MakeItem(GameObject block)
    {
        if (UnityEngine.Random.Range(0, 5) == 0)
        {
            GameObject item = null;

            int num = UnityEngine.Random.Range(0, 3);
            switch (num)
            {
                case 0:
                {
                    item = Instantiate(BombPowerItemPrefab);
                    break;
                }                  
                case 1:
                {
                    item = Instantiate(BombCountItemPrefab);
                    break;
                }
                case 2:
                {
                    item = Instantiate(SpeedItemPrefab);
                    break;
                }
            }
            if (item != null)
            {
                item.transform.position = block.transform.position;
            }          
        }
    }
}
public class DirValue
{
    public int fireRange = 5;
    public BlockType blockType;
    public GameObject destroyBrick;
}
