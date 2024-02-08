using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.ParticleSystem;
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
    public GameObject LifeItemPrefab;
    public GameObject TimeItemPrefab;

    public WaitForSeconds boomSecond = new WaitForSeconds(1);
    public Dictionary<Direction, DirValue> fireContainer;
    public BoxCollider2D myCollider;
    public GameObject boomImage;
    public ParticleSystem myParticle;
    private int actionCount = 0;
    public void InIt()
    {
        //
        //GetComponent<BoxCollider2D>().enabled = true;
        //
        myCollider.isTrigger = true;
        boomImage.SetActive(true);
        actionCount = 0;
        fireContainer = new Dictionary<Direction, DirValue>();
        fireContainer.Add(Direction.up, new DirValue());
        fireContainer.Add(Direction.down, new DirValue());
        fireContainer.Add(Direction.left, new DirValue());
        fireContainer.Add(Direction.right, new DirValue());
        for (int i = 0; i < BoomEffectsUp.Count; i++) BoomEffectsUp[i].InIt(DisAbleEffectCollider);
        for (int i = 0; i < BoomEffectsDown.Count; i++) BoomEffectsDown[i].InIt(DisAbleEffectCollider);
        for (int i = 0; i < BoomEffectsLeft.Count; i++) BoomEffectsLeft[i].InIt(DisAbleEffectCollider);
        for (int i = 0; i < BoomEffectsRight.Count; i++) BoomEffectsRight[i].InIt(DisAbleEffectCollider);
        StartCoroutine(StartBoom());
    }
    private void DisEnabledCollider()
    {
        for (int i = 0; i < BoomEffectsUp.Count; i++) BoomEffectsUp[i].myCollider.enabled = false;
        for (int i = 0; i < BoomEffectsDown.Count; i++) BoomEffectsDown[i].myCollider.enabled = false;
        for (int i = 0; i < BoomEffectsLeft.Count; i++) BoomEffectsLeft[i].myCollider.enabled = false;
        for (int i = 0; i < BoomEffectsRight.Count; i++) BoomEffectsRight[i].myCollider.enabled = false;
    }
    public void DisAbleEffectCollider()
    {
        if (actionCount > 0)
            return;
        OnAndOffCollider(false);
    }
    public IEnumerator StartBoom(bool _isNow = false)
    {
        if (!_isNow)
            yield return new WaitForSeconds(2);
        SoundManager.instance.PlaySfx(SoundManager.Sfx.bomb);
        boomImage.SetActive(false);
        myParticle.Play();
        //
        //GetComponent<BoxCollider2D>().enabled = false;
        //
        SetBoomRange();
        OnAndOffCollider(true);
        BoomDirectionProcess(Direction.up, BoomEffectsUp);
        BoomDirectionProcess(Direction.down, BoomEffectsDown);
        BoomDirectionProcess(Direction.left, BoomEffectsLeft);
        BoomDirectionProcess(Direction.right, BoomEffectsRight);
        yield return new WaitForSeconds(0.2f);
        DisEnabledCollider();
        yield return new WaitForSeconds(0.8f);
        StopCoroutine(StartBoom());
        gameObject.SetActive(false);
        GameManager.instance.playerFire.BombCount++;
    }
    public void OnAndOffCollider(bool _bool)
    {
        for (int i = 0; i < fireContainer[Direction.up].fireRange; i++)
        {
            BoomEffectsUp[i].isfire = _bool;
            BoomEffectsUp[i].myCollider.enabled = _bool;
        }
        for (int i = 0; i < fireContainer[Direction.down].fireRange; i++)
        {
            BoomEffectsDown[i].isfire = _bool;
            BoomEffectsDown[i].myCollider.enabled = _bool;
        }
        for (int i = 0; i < fireContainer[Direction.left].fireRange; i++)
        {
            BoomEffectsLeft[i].isfire = _bool;
            BoomEffectsLeft[i].myCollider.enabled = _bool;
        }
        for (int i = 0; i < fireContainer[Direction.right].fireRange; i++)
        {
            BoomEffectsRight[i].isfire = _bool;
            BoomEffectsRight[i].myCollider.enabled = _bool;
        }
    }
    private void BoomDirectionProcess(Direction _direction,List<BoomEffectController> _boomEffecter)
    {
        for (int i = 0; i < fireContainer[_direction].fireRange; i++)
        {
            _boomEffecter[i].boomparticle.Play();
        }
        if (fireContainer[_direction].blockType == BlockType.brick)
        {
            _boomEffecter[fireContainer[_direction].fireRange].boomparticle.Play();
            MakeItem(_boomEffecter[fireContainer[_direction].fireRange].destroyBrick);
            Destroy(_boomEffecter[fireContainer[_direction].fireRange].destroyBrick);
            SoundManager.instance.PlaySfx(SoundManager.Sfx.brick);
        }
    }
    private void SetBoomRange()
    {
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
        if (block == null)
            return;
        if (UnityEngine.Random.Range(0, 5) == 0)
        {
            GameObject item = null;

            int num = UnityEngine.Random.Range(0, 11);
            switch (num)
            {
                case int n when n < 3:
                {
                    item = Instantiate(BombPowerItemPrefab);
                    break;
                }
                case int n when n >= 3 && n < 6:
                {
                    item = Instantiate(BombCountItemPrefab);
                    break;
                }
                case int n when n >= 6 &&n < 9:
                {
                    item = Instantiate(SpeedItemPrefab);
                    break;
                }
                case 9:
                {
                    item = Instantiate(TimeItemPrefab);
                    break;
                }
                case 10:
                {
                    item = Instantiate(LifeItemPrefab); 
                    break;
                }
            }
            if (item != null)
            {
                item.transform.position = block.transform.position;
            }          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            myCollider.isTrigger = false;
        
    }
}
public class DirValue
{
    public int fireRange = 5;
    public BlockType blockType;
    public GameObject destroyBrick;
}
