using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    BombPower = 0,
    BombCount = 1,
    Speed = 2
}

public class Item : MonoBehaviour
{
    public ItemType IType;
    PlayerFire playerFire;
    PlayerMove playerMove;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerFire = otherCollider.GetComponent<PlayerFire>();
            playerMove = otherCollider.GetComponent<PlayerMove>();

            if (IType == ItemType.BombPower)
            {
                playerFire.BombPower++;
                Debug.Log(playerFire.BombPower);
            }

            else if (IType == ItemType.BombCount)
            {
                playerFire.MaxBombCount++;
                Debug.Log(playerFire.MaxBombCount);
            }

            else if (IType == ItemType.Speed)
            {
                playerMove._speed++;
                Debug.Log(playerMove._speed);
            }
            Destroy(this.gameObject);
        }
    }
}