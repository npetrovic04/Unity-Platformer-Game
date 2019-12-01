using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prikazuje loading screen
/// </summary>
public class LoadingCtrl : MonoBehaviour
{
    public GameObject panelLoading_1;
    public GameObject panelLoading_2;
    public GameObject panelLoading_3;
    public GameObject panelLoading_4;
    public GameObject panelLoading_5;
    public GameObject panelLoading_6;
    public GameObject panelLoading_7;
    public GameObject panelLoading_8;
    public GameObject panelLoading_9;
    public GameObject panelLoading_10;
    public static LoadingCtrl instance;


    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowLoading(string levelNumber)
    {
        if (levelNumber.Equals("1"))
        {
            panelLoading_1.SetActive(true);
        }
        else if (levelNumber.Equals("2"))
        {
            panelLoading_2.SetActive(true);
        }
        else if (levelNumber.Equals("3"))
        {
            panelLoading_3.SetActive(true);
        }
        else if (levelNumber.Equals("4"))
        {
            panelLoading_4.SetActive(true);
        }
        else if (levelNumber.Equals("5"))
        {
            panelLoading_5.SetActive(true);
        }
        else if (levelNumber.Equals("6"))
        {
            panelLoading_6.SetActive(true);
        }
        else if (levelNumber.Equals("7"))
        {
            panelLoading_7.SetActive(true);
        }
        else if (levelNumber.Equals("8"))
        {
            panelLoading_8.SetActive(true);
        }
        else if (levelNumber.Equals("9"))
        {
            panelLoading_9.SetActive(true);
        }
        else if (levelNumber.Equals("10"))
        {
            panelLoading_10.SetActive(true);
        }
    }
}
