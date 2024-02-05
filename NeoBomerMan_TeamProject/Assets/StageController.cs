using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        GameObject playerObj = Instantiate(player);
        playerObj.transform.position = new Vector2(-6, 4.5f);
        GameManager.instance.player = playerObj.GetComponent<Player>();
        GameManager.instance.playerMove = playerObj.GetComponent<PlayerMove>();
        GameManager.instance.playerFire = playerObj.GetComponent<PlayerFire>();
        GameManager.instance.SetPlayerState();
    }
}
