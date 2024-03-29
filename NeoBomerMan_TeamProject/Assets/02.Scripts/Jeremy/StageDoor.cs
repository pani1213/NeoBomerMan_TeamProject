using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDoor : MonoBehaviour
{
    public BoxCollider2D myCollider;
    public Sprite doorOpen;
    public SpriteRenderer mySpriteRanderer;
    public int openScore = 4;
    public void DoorOpen()
    {
        mySpriteRanderer.sprite = doorOpen;
        myCollider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySfx(SoundManager.Sfx.StageClear);
            GameManager.instance.GetPlayerState();

            GameManager.instance.isInput = false;
            GameManager.instance.playerMove.myAnimation.Play("PlayerDie");
            GameManager.instance.isTimeCheck = false;
            GameManager.instance.StartCoroutine(GameManager.instance.SetScoreBord());

        }
    }   
    
}
