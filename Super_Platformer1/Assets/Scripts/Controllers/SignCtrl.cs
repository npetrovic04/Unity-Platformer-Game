using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCtrl : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameCtrl.instance.StopCameraFollow(other.gameObject);

            GameCtrl.instance.ActivateEnemySpawner();
        }
    }
}
