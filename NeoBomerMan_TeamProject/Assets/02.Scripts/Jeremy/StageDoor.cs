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
            GameManager.instance.GetPlayerState();
            Scene scene = SceneManager.GetActiveScene();
            int sceneindex = scene.buildIndex;
            SceneManager.LoadScene(++sceneindex);
        }
    }

   
}
