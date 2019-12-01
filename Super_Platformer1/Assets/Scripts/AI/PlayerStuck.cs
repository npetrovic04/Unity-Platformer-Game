using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuck : MonoBehaviour
{
    public GameObject player;           // to access the PlayerCtrl script

    PlayerCtrl playerCtrl;              // a reference to the PlayerCtrl script


    void Start ()
    {
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerCtrl.isStuck = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        playerCtrl.isStuck = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerCtrl.isStuck = false;
    }
}
