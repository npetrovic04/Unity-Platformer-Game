using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{

    public int coinCount;           // keeps track of the number of coins picked up
    public int lives;               // follows the lives of players
    public int score;               // follow the score
    public int highScore;
    public bool[] keyFound;         // to track what keys were found
    public LevelData[] levelData;   // for tracking level given as unlocked, won stars, number of levels
    public bool isFirstBoot;        // for initialization given when the game was first started

    public bool playSound;          // to toggle sound
    public bool playMusic;          // to toggle music
}
