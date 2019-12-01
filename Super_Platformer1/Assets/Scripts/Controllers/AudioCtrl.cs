using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Audio u igri
/// </summary>
public class AudioCtrl : MonoBehaviour
{

    public static AudioCtrl instance;               // to invoke the public method in this script
    public PlayerAudio playerAudio;                 
    public AudioFX audioFX;
    public Transform player;                        // we need this to play sound at the player position
    public GameObject BGMusic;
    public bool bgMusicOn;                          // turn on / off the music
    public GameObject btnSound, btnMusic;           // Sounds & Music toggle buttons in the Pause Menu
    public Sprite imgSoundOn, imgSoundOff;
    public Sprite imgMusicOn, imgMusicOff;

    [Tooltip("soundOn se koristi da se pali/gasi sound iz inspektora")]
    public bool soundOn;

    // Use this for initialization
    void Start ()
    {
        if (instance == null)
            instance = this;

        if (DataCtrl.instance.data.playMusic)
        {
            BGMusic.SetActive(true);

            btnMusic.GetComponent<Image>().sprite = imgMusicOn;
        }
        else
        {
            BGMusic.SetActive(false);

            btnMusic.GetComponent<Image>().sprite = imgMusicOff;
        }

        if (DataCtrl.instance.data.playSound)
        {
            soundOn = true;

            btnSound.GetComponent<Image>().sprite = imgSoundOn;
        }
        else
        {
            soundOn = false;

            btnSound.GetComponent<Image>().sprite = imgSoundOff;
        }
    }

    public void PlayerJump(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerJump, playerPos);
        }
    }

    public void CoinPickup(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.coinPickup, playerPos);
        }
    }

    public void FireBullets(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.fireBullets, playerPos);
        }
    }

    public void EnemyExplosion(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyExplotion, playerPos);
        }
    }

    public void BreakableCreates(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.breakCrates, playerPos);
        }
    }

    public void WaterSplash(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.waterSplash, playerPos);
        }
    }

    public void PowerUp(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.powerUp, playerPos);
        }
    }

    public void KeyFound(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.keyFound, playerPos);
        }
    }

    public void EnemyHit(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyHit, playerPos);
        }
    }

    public void PlayerDied(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerDied, playerPos);
        }
    }

    public void ToggleSound()
    {
        if (DataCtrl.instance.data.playSound)
        {
            soundOn = false;

            // set the Music off image to the music button
            btnSound.GetComponent<Image>().sprite = imgSoundOff;

            // save the change to the database
            DataCtrl.instance.data.playSound = false;
        }
        else
        {
            soundOn = false;

            // set the Music off image to the music button
            btnSound.GetComponent<Image>().sprite = imgSoundOn;

            // save the change to the database
            DataCtrl.instance.data.playSound = true;
        }
    }

    public void ToggleMusic()
    {
        if (DataCtrl.instance.data.playMusic)
        {
            // stop the BG Music
            BGMusic.SetActive(false);

            // set the Music off image to the music button
            btnMusic.GetComponent<Image>().sprite = imgMusicOff;

            // save the change to the database
            DataCtrl.instance.data.playMusic = false;
        }
        else
        {
            // play the BG Music
            BGMusic.SetActive(true);

            // set the Music on image to the music button
            btnMusic.GetComponent<Image>().sprite = imgMusicOn;

            // save the change to the database
            DataCtrl.instance.data.playMusic = true;
        }
    }
}
