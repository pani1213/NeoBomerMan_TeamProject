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
        Debug.Log(SceneManager.sceneCount);
        mySpriteRanderer.sprite = doorOpen;
        myCollider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int scene = SceneManager.sceneCount;
            Debug.Log(scene);
            SceneManager.LoadScene(++scene);
        }
    }

   
}
