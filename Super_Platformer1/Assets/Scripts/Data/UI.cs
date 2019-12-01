using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Keeps essential Gameplay items such as score, level reset, save / load data, updating HUD ...
/// </summary>

[Serializable]
public class UI 
{
    [Header("Text")]
    public Text txtCoinCount;       // for counting the number of coins that have been collected
    public Text txtScore;           // for showing score in HUD
    public Text txtTimer;           // shows the timer in the HUD

    [Header("Images/Sprites")]
    public Image key0;
    public Image key1;
    public Image key2;
    public Sprite key0Full;
    public Sprite key1Full;
    public Sprite key2Full;
    public Image heart1;            // represents one player's life
    public Image heart2;
    public Image heart3;
    public Sprite emptyHeart;       // shows when life is lost
    public Sprite fullHeart;        // shows when lives are full
    public Slider bossHealth;       // boss health meter

    [Header("Popup Menus/Panels")]
    public GameObject panelGameOver;
    public GameObject levelCompleteMenu;        // is displayed when the level is spinning
    public GameObject panelMobileUI;            // contains mobile buttons
    public GameObject panelPause;               // pause menu
    
}
