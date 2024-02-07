using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas myCanvas;
    public GameObject scoreBord, gameOver;
    public Image bombImage;
    public Text life, score, timer, notice,boomCount,boomRange,Speed , scoreBordTime, scoreBordBonus, scoreBordTotal;
    public Sprite[] bombSprite;
    public void InIt()
    {
        GameManager.instance.canvasController = this;
        myCanvas = GetComponent<Canvas>();
        myCanvas.worldCamera = Camera.main;
        GameManager.instance.SetScoer(0);
        life.text = GameManager.instance.playerLife.ToString();
        GameManager.instance.isTimeCheck = true;
        GameManager.instance.StartTimer();
    }
}
