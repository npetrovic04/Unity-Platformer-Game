using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossOneAI : MonoBehaviour
{
    public float jumpSpeed;                     // boss jumping speed along the Y axis
    public int startJumpingAt;                  // the level of health at which the boss starts to jump
    public int jumpDelay;                       // delay before the next jump
    public int health;                          // health boss
    public Slider bossHealth;                   // health metar level boss
    public GameObject bossBullet;               // a bullet fired by a boss
    public float delayBeforeFiring;             // delay before firing a bullet

    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector3 bulletSpawnPos;                     // the position from where the bullet should be fired
    bool canFire, isJumping;                    // check when the boss can jump and shoot

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        canFire = false;

        bulletSpawnPos = gameObject.transform.Find("BulletSpawnPos").transform.position;

        Invoke("Reload", Random.Range(1f, delayBeforeFiring));
    }
	
	void Update ()
    {
        if (canFire)
        {
            FireBullets();
            canFire = false;

            if (health < startJumpingAt && !isJumping)
            {
                InvokeRepeating("Jump", 0, jumpDelay);
                isJumping = true;                           // this is to ensure that if condition is only true once, then InvokeRepeating does its thing
            }
        }
    }

    void Reload()
    {
        canFire = true;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }

    void FireBullets()
    {
        Instantiate(bossBullet, bulletSpawnPos, Quaternion.identity);

        Invoke("Reload", delayBeforeFiring);
    }

    void RestoreColor()
    {
        sr.color = Color.white;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            if (health == 0)
                GameCtrl.instance.BulletHitEnemy(gameObject.transform);

            if (health > 0)
            {
                health--;

                bossHealth.value = (float)health;

                sr.color = Color.red;

                Invoke("RestoreColor", 0.1f);
            }
        }
    }
}
