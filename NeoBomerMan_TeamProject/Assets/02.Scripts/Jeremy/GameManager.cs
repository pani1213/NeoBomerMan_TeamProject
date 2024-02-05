using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    public int playerLife = 2, playerBoomRange = 1, playerBoomCount = 1, player_speed = 200,gameScore = 0;
    public string gameTimer = "";
    public PlayerMove playerMove;
    public PlayerFire playerFire;
    public Player player;
    public CanvasController canvasController;
    public bool isInput = true;
    //public List<BoomController> addBooms = new List<BoomController>();
    //public List<BoomController> finalBooms = new List<BoomController>();
    private void Start()
    {
        SoundManager.instance.PlaySfx(SoundManager.Sfx.Start);       
        SoundManager.instance.PlayBgm(true);
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
        //int scene =  SceneManager.sceneCount;
        SceneManager.LoadScene(SceneManager.sceneCount);
    }
    public void SetScoer(int _scoer)
    {
        gameScore += _scoer;
        canvasController.score.text = gameScore.ToString();
    }
}
