using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    BombPower = 0,
    BombCount = 1,
    Speed = 2,
    Time = 3,
    Life = 4
}
public class Item : MonoBehaviour
{
    public ItemType IType;
    PlayerFire playerFire;
    PlayerMove playerMove;
    Player player;
    private const int boomMaxCount = 5, speedMaxCount = 3;
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerFire = otherCollider.GetComponent<PlayerFire>();
            playerMove = otherCollider.GetComponent<PlayerMove>();
            player = otherCollider.GetComponent<Player>();

            if (IType == ItemType.BombPower)
            {
                if (playerFire.BombPower < boomMaxCount)
                    playerFire.BombPower++;
            }
            else if (IType == ItemType.BombCount)
            {
                playerFire.MaxBombCount++;
                playerFire.BombCount = playerFire.MaxBombCount;
                Debug.Log(playerFire.MaxBombCount);
            }
            else if (IType == ItemType.Speed)
            {
                if (playerMove._speed < speedMaxCount)
                    playerMove._speed += 0.5f;
            }
            else if (IType == ItemType.Time)
            {
                GameManager.instance.TimePlus(30);
                GameManager.instance.SetTimer();
            }
            else if (IType == ItemType.Life)
            {
                player.PlayerHealth++;
                GameManager.instance.SetLife();
            }
            Destroy(this.gameObject);
        }
    }
}
