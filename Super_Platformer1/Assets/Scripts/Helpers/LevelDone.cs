using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays the Level Complete menu
/// </summary>
public class LevelDone : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.LevelComplete();

            GameCtrl.instance.DeleteCheckPoints();
              
        }
    }
}
