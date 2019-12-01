using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the coin behavior when the player interacts with it
/// </summary>
public class CoinCtrl : MonoBehaviour
{
    public enum CoinFX
    {
        Vanish,
        Fly
    }

    public CoinFX coinFX;           
    public float speed;         // the speed at which coins fly
    public bool startFlying;    // if true, the coin will start flying to the HUD when they are collected

    GameObject coinMeter;        // collects coins in HUD

    void Start()
    {
        startFlying = false;

        if (coinFX == CoinFX.Fly)
        {
            coinMeter = GameObject.Find("img_Coin_Count");
        }
    }

    void Update()
    {
        if (startFlying)
        {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (coinFX == CoinFX.Vanish)
                Destroy(gameObject);      // destroys coin
            else if (coinFX == CoinFX.Fly)
            {
                gameObject.layer = 0;
                startFlying = true;
            }
        }
    }
}
