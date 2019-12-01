using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls the behavior of the enemy when the player jumps on his head
/// </summary>
public class EnemyHeadControl : MonoBehaviour
{
    public GameObject enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerFeet"))
        {
            GameCtrl.instance.PlayerStompsEnemy(enemy);

            AudioCtrl.instance.EnemyHit(gameObject.transform.position);

            SFXCtrl.instance.ShowEnemyPoof(enemy.transform.position);
        }
    }
}
