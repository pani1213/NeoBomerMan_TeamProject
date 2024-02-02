using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    BombPower = 0,
    BombCount = 1,
    Speed = 2,
    Glove = 3,
    Shoe = 4
}
public class Item : MonoBehaviour
{
    public ItemType IType;
    PlayerFire playerFire;
    PlayerMove playerMove;
    private const int boomMaxCount = 5, speedMaxCount = 3;
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerFire = otherCollider.GetComponent<PlayerFire>();
            playerMove = otherCollider.GetComponent<PlayerMove>();

            if (IType == ItemType.BombPower)
            {
                if (playerFire.BombPower < boomMaxCount)
                    playerFire.BombPower++;
        
            }
            else if (IType == ItemType.BombCount)
            {
                playerFire.MaxBombCount++;

            }

            else if (IType == ItemType.Speed)
            {
                if(playerMove._speed < speedMaxCount)
                playerMove._speed++;
              
            }

            else if (IType == ItemType.Glove)
            {
                playerFire.GloveItem = true;
            }

            else if (IType == ItemType.Shoe)
            {
                playerFire.ShoeItem = true;
            }
            Destroy(this.gameObject);
        }
    }
}
