using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Contains audio clips for the player
/// </summary>
[Serializable]
public class PlayerAudio
{
    [Header("Part 1")]
    public AudioClip playerJump;
    public AudioClip coinPickup;
    public AudioClip fireBullets;
    public AudioClip enemyExplotion;
    public AudioClip breakCrates;

    [Header("Part 2")]
    public AudioClip waterSplash;
    public AudioClip powerUp;
    public AudioClip keyFound;
    public AudioClip enemyHit;
    public AudioClip playerDied;
}
