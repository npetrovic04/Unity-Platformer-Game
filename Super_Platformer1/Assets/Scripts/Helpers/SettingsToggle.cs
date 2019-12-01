using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SettingsToggle : MonoBehaviour
{
    public RectTransform btnL, btnA, btnFB, btnT, btnG, btnR;
    public float moveL, moveA, moveFB, moveT, moveG, moveR;
    public float defaultPosY, defaultPosX;
    public float speed;

    bool expanded;

	void Start ()
    {
        expanded = false;           // the buttons are hidden when the game starts, so it is set to false
    }

    public void Toggle()
    {
        if (!expanded)
        {
            // show buttons
            btnL.DOAnchorPosY(moveL, speed, false);
            btnA.DOAnchorPosY(moveA, speed, false);
            btnFB.DOAnchorPosY(moveFB, speed, false);
            btnT.DOAnchorPosY(moveT, speed, false);
            btnG.DOAnchorPosY(moveG, speed, false);
            btnR.DOAnchorPosY(moveR, speed, false);
            
            expanded = true;
        }
        else
        {
            btnL.DOAnchorPosY(defaultPosY, speed, false);
            btnA.DOAnchorPosY(defaultPosY, speed, false);
            btnFB.DOAnchorPosY(defaultPosY, speed, false);
            btnT.DOAnchorPosY(defaultPosY, speed, false);
            btnG.DOAnchorPosY(defaultPosY, speed, false);
            btnR.DOAnchorPosY(defaultPosY, speed, false);
           
            expanded = false;
        }
    }
}
