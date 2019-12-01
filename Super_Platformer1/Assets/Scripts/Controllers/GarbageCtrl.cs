using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It destroys every gameobject that comes into contact with it except the player
/// For the player, the level restarts
/// </summary>
public class GarbageCtrl : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.PlayerDied(other.gameObject);
        }
        //else
        //{
        //    Destroy(other.gameObject);
        //}
    }
}
