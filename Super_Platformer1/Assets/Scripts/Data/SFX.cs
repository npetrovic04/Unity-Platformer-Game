using System.Collections;
using System;
using UnityEngine;

/// <summary>
/// groups the particles effects used in the game
/// </summary>

[Serializable]
public class SFX 
{

    public GameObject sfx_coin_pickup;          // is displayed when the player picks up coins
    public GameObject sfx_bullet_pickup;        // is displayed when the player picks up the Powerup key
    public GameObject sfx_playerLands;          // is displayed when the player touches the surface after the jump
    public GameObject sfx_fragment_1;           // box fragments are shown when crashes break
    public GameObject sfx_fragment_2;
    public GameObject sfx_splash;               // the splash effect
    public GameObject sfx_enemy_explosion;      // is displayed when a player bullet hits an enemy
    public GameObject sfx_Fireworks_1;
}
