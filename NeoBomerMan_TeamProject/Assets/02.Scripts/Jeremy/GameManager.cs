using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    public int playerLife = 2, playerBoomRange = 1, playerBoomCount = 1, gameScore = 0,stageScore = 0,scoreBordBonus = 0;
    public float player_speed = 1;
    private float inGameTimer = 120, maxGameCount = 120;
    
    public string gameTimer = "";
    public PlayerMove playerMove;
    public PlayerFire playerFire;
    public Player player;
    public CanvasController canvasController;
    public bool isInput = true, isTimeCheck = false;
    private float oneSecond =0;
    public float Cool_time = 2;
    private bool isPlay = false , isNextStage = false;
    public bool isHurry = false, isGameOver = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1366, 768, false);
    }
    public void Update()
    {
        IngameTimer();
        if (isPlay)
            Cool_time -= Time.deltaTime;

        if (Input.anyKeyDown && isNextStage)
        {
            isNextStage = false;
            Scene scene = SceneManager.GetActiveScene();
            int sceneindex = scene.buildIndex;
            SceneManager.LoadScene(++sceneindex);
        }
        if (Input.anyKeyDown && isGameOver)
        {
            //isGameOver = false;
            //isInput = true;
            //playerLife = 2;
            //playerBoomRange = 1;
            //playerBoomCount = 1;
            //gameScore = 0;
            //stageScore = 0;
            //player_speed = 1;
            //scoreBordBonus = 0;
            //SceneManager.LoadScene(1);
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Scene scene = SceneManager.GetActiveScene();
            int sceneindex = scene.buildIndex;
            SceneManager.LoadScene(++sceneindex);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        Destroy(SoundManager.instance.gameObject);
        Destroy(gameObject);

    }
    public void FindCanvas()
    {
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
    }
    public void StartTimer()
    {
        isTimeCheck = true;
        inGameTimer = maxGameCount;
    }
    private void IngameTimer()
    {
        if (!isTimeCheck)
            return;
        if (inGameTimer == 60)
        {

            SoundManager.instance.PlayBgm(SoundManager.Bgm.alarm);
            isHurry = true;
            isPlay = true;
            canvasController.notice.gameObject.SetActive(true);
            canvasController.notice.text = "Hurry Up";

        }
        if (Cool_time < 0)
        {
            if (isHurry)
                SoundManager.instance.PlayBgm(SoundManager.Bgm.hurryUp);
            canvasController.notice.gameObject.SetActive(false);
            Cool_time = 2f;
            isPlay = false;
        }

        if (oneSecond <= 0)
        {
            oneSecond = 1;
            inGameTimer--;
            SetTimer();
        }
        if (inGameTimer <= 0)
        {
            isPlay = true;
            canvasController.notice.gameObject.SetActive(true);
            canvasController.notice.text = "Time Up";
            inGameTimer = maxGameCount;
            playerMove.PlayerDie();
        }
        oneSecond -= Time.deltaTime;
    }

    public IEnumerator SetScoreBord()
    {
        SoundManager.instance.PlayBgm(SoundManager.Bgm.victory);
        canvasController.scoreBord.SetActive(true);
        canvasController.scoreBordTime.text = TimeSpan.FromSeconds(inGameTimer).ToString(@"mm\:ss");
        canvasController.scoreBordTotal.text = stageScore.ToString();
        while (inGameTimer > 0) 
        {
            inGameTimer--;
            canvasController.scoreBordTime.text = TimeSpan.FromSeconds(inGameTimer).ToString(@"mm\:ss");
            scoreBordBonus += 300;
            canvasController.scoreBordBonus.text = scoreBordBonus.ToString();
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1);
        stageScore += scoreBordBonus;
        canvasController.scoreBordTotal.text = stageScore.ToString();
        if (stageScore <= 28000)
        {
            Debug.Log(stageScore);
            canvasController.bombImage.sprite = canvasController.bombSprite[0];
        }
        else if (stageScore <= 31000)
        { 
            Debug.Log(stageScore);
            canvasController.bombImage.sprite = canvasController.bombSprite[1];
        }
        else
        {
            Debug.Log(stageScore);
            canvasController.bombImage.sprite = canvasController.bombSprite[2];
        }
        SetScoer(stageScore);
        scoreBordBonus = 0;
        stageScore = 0;
        canvasController.scoreBordBonus.text = scoreBordBonus.ToString();
        yield return new WaitForSeconds(1);
        canvasController.bombImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        isNextStage = true;

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
    public void SetCanvasState()
    {
        canvasController.boomCount.text = playerFire.MaxBombCount.ToString();
        canvasController.boomRange.text = playerFire.BombPower.ToString();
        canvasController.Speed.text = playerMove._speed.ToString();
    }
    public void ButtonActionNextScene()
    {
        SceneManager.LoadScene(SceneManager.sceneCount);
    }
    public void SetScoer(int _scoer)
    {
        gameScore += _scoer;
        stageScore += _scoer;
        canvasController.score.text = gameScore.ToString();
    }
    public void SetLife()
    {
        playerLife = player.PlayerHealth;
        canvasController.life.text = playerLife.ToString();
    }
    public void SetTimer()
    {
        if (canvasController == null)
            return;
        canvasController.timer.text = TimeSpan.FromSeconds(inGameTimer).ToString(@"mm\:ss");
    }
    public void TimePlus(float plusTime)
    {   
        inGameTimer += plusTime;
    }
}
