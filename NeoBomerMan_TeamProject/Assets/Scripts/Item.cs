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

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        PlayerFire playerFire = otherCollider.GetComponent<PlayerFire>();
        Player player = otherCollider.GetComponent<Player>();

        if (IType == ItemType.BombPower)
        {
            playerFire.BombPower++;
        }

        else if (IType == ItemType.BombCount)
        {
            playerFire.MaxBombCount++;
        }

        else if (IType == ItemType.Speed)
        {
            player.PlayerSpeed++;
        }

        Destroy(this.gameObject);
    }
}
