using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Routes the mobile input to the correct methods in the PlayerCtrl script
/// </summary>
public class MobileUICtrl : MonoBehaviour
{
    public GameObject player;

    PlayerCtrl playerCrtl;

    void Start()
    {
        playerCrtl = player.GetComponent<PlayerCtrl>();
    }

    public void MobileMoveLeft()
    {
        playerCrtl.MobileMoveLeft();
    }

    public void MobileMoveRight()
    {
        playerCrtl.MobileMoveRight();
    }

    public void MobileStop()
    {
        playerCrtl.MobileStop();
    }

    public void MobileFireBullets()
    {
        playerCrtl.MobileFireBullets();
    }

    public void MobileJump()
    {
        playerCrtl.MobileJump();
    }
}
