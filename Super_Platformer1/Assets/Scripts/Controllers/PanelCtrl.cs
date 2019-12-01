using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelCtrl : MonoBehaviour
{
    //public static PanelCtrl instance;

    public GameObject panelTutorial;
    

    public void ShowTutorialPanel()
    {
        //panelTutorial.SetActive(true);

        panelTutorial.gameObject.GetComponent<RectTransform>().DOAnchorPosY(-60, 0.7F, false);
    }

    public void HideTutorialPanel()
    {
        //panelTutorial.SetActive(false);

        panelTutorial.gameObject.GetComponent<RectTransform>().DOAnchorPosY(700, 0.7F, false);
    }

}
