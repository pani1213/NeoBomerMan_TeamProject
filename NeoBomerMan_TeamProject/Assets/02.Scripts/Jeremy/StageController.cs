using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public SoundManager.Bgm Bgm;
    public GameObject player;
    void Start()
    {
        SoundManager.instance.PlayBgm(Bgm);
        if (Bgm == SoundManager.Bgm.ending)
            return;
        GameObject playerObj = Instantiate(player);
        playerObj.transform.position = new Vector2(-6, 4.5f);
        GameManager.instance.player = playerObj.GetComponent<Player>();
        GameManager.instance.playerMove = playerObj.GetComponent<PlayerMove>();
        GameManager.instance.playerFire = playerObj.GetComponent<PlayerFire>();
        GameManager.instance.isInput = true;
        GameManager.instance.SetPlayerState();
        GameManager.instance.FindCanvas();
        GameManager.instance.SetCanvasState();
        GameManager.instance.canvasController.InIt();
    }
}
