using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// particle effect when the player lands
/// </summary>
public class FeetCtrl : MonoBehaviour
{
    PlayerCtrl playerCtrl;
    public Transform dustParticlePos;
    public GameObject player;


    void Start()
    {
        playerCtrl = gameObject.transform.parent.gameObject.GetComponent<PlayerCtrl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position);

            playerCtrl.isJumping = false;
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position);

            playerCtrl.isJumping = false;

            player.transform.parent = other.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
        }
    }
}
