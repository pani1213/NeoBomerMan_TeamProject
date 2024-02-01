using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerMove playerMove;
    public PlayerFire playerFire;
    public Player player;

    private void Start()
    {
        SoundManager.instance.PlaySfx(SoundManager.Sfx.Start);       
        SoundManager.instance.PlayBgm(true);
    }
}
