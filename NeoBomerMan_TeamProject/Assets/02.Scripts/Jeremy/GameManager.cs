using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    public int playerLife = 2, playerBoomRange = 1, playerBoomCount = 1, player_speed = 200, gameScore = 0;
    private float inGameTimer = 120, maxGameCount = 120;
    
    public string gameTimer = "";
    public PlayerMove playerMove;
    public PlayerFire playerFire;
    public Player player;
    public CanvasController canvasController;
    public bool isInput = true, isTimeCheck = false;
    private float oneSecond =0;
    private void Start()
    {
        SoundManager.instance.PlaySfx(SoundManager.Sfx.Start);
        SoundManager.instance.PlayBgm(true);
    }
    public void Update()
    {
        IngameTimer();
    }
    public void StartTimer()
    {
        isTimeCheck = true;
        inGameTimer = maxGameCount;
        IngameTimer();
    }
    private void IngameTimer()
    {
        if (!isTimeCheck)
            return;

        if (oneSecond <= 0)
        {
            oneSecond = 1;
            inGameTimer--;
            SetTimer();
        }
        if (inGameTimer <= 0)
        {
            inGameTimer = maxGameCount;
            playerMove.PlayerDie();
        }
        oneSecond -= Time.deltaTime;
    }
    public void GetPlayerState()
    {
        playerLife = player.PlayerHealth;
        playerBoomRange = playerFire.BombPower;
        playerBoomCount = playerFire.MaxBombCount;
        player_speed = playerMove._speed;
    }
    public void SetPlayerState()
    {
        player.PlayerHealth = playerLife;
        playerFire.BombPower = playerBoomRange;
        playerFire.MaxBombCount = playerBoomCount;
        playerMove._speed = player_speed;
    }
    public void ButtonActionNextScene()
    {
        SceneManager.LoadScene(SceneManager.sceneCount);
    }
    public void SetScoer(int _scoer)
    {
        gameScore += _scoer;
        canvasController.score.text = gameScore.ToString();
    }
    public void SetLife()
    {
        playerLife = player.PlayerHealth;
        canvasController.life.text = playerLife.ToString();
    }
    public void SetTimer()
    {
        canvasController.timer.text = TimeSpan.FromSeconds(inGameTimer).ToString(@"mm\:ss");
    }
    public void TimePlus(float plusTime)
    {   
        inGameTimer += plusTime;
    }
}
