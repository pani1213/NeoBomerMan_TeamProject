using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas myCanvas;
    public Text life, score, timer;
    private void Start()
    {
        GameManager.instance.canvasController = this;
        myCanvas = GetComponent<Canvas>();
        myCanvas.worldCamera = Camera.main;

        GameManager.instance.SetScoer(0);
        life.text = GameManager.instance.playerLife.ToString();
        timer.text = GameManager.instance.gameTimer;
    }
}
