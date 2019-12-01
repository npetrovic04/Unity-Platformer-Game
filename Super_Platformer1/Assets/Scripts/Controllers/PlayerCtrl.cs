using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    [Tooltip("ovo je integer koji odredjuje ubrzanje kretanja")]
    public int SpeedBoost;
    public float JumpSpeed;
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public float boxWidth;
    public float boxHeight;
    public float delayForDoubleJump;
    public LayerMask whatIsGround;
    public Transform leftBulletSpawnPos, rightBulletSpawnPos;
    public GameObject leftBullet, rightBullet;
    public bool SFXOn;
    public bool canFire;
    public bool isJumping;
    public bool isStuck;

    public GameObject btn_Fire;

    bool canDoubleJump;
    public bool leftPressed, rightPressed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    void Awake()
    {
        if (PlayerPrefs.HasKey("CPX"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("CPX"), PlayerPrefs.GetFloat("CPY"), transform.position.z);
        }
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGround);

        float playerSpeed = Input.GetAxisRaw("Horizontal");     //the value will be one of three values: 1, -1, or 0
        playerSpeed *= SpeedBoost;

        if (playerSpeed != 0)                                         //  these commands do not allow the Jump () method below to perform the animation to return
            MoveHorizontal(playerSpeed);                              //  state in Idle after the jump. So let's introduce bool IsJumping. And we tell him
        else                                                          //  only if the Jump button is not pressed, ie if it is not in the jump, then yes
            StopMoving();                                             //  running, idle ...
        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Fire1"))
        {
            FireBullets();
        }

        ShowFalling();

        if (leftPressed)
            MoveHorizontal(-SpeedBoost);

        if (rightPressed)
            MoveHorizontal(SpeedBoost);


    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(feet.position, feetRadius);

        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
    }

    void MoveHorizontal(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

        if (playerSpeed < 0)       
            sr.flipX = true;
        else if (playerSpeed > 0)
            sr.flipX = false;

        if (!isJumping)
            anim.SetInteger("State", 1);
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if (!isJumping)
            anim.SetInteger("State", 0);
    }

    void ShowFalling()
    {
        if (rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, JumpSpeed));
            anim.SetInteger("State", 2);

            // lean jump sound
            AudioCtrl.instance.PlayerJump(gameObject.transform.position);

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }

        if (canDoubleJump && !isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, JumpSpeed));
            anim.SetInteger("State", 2);

            // lean jump sound
            AudioCtrl.instance.PlayerJump(gameObject.transform.position);

            canDoubleJump = false;
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void FireBullets()
    {
        if (canFire)
        {
            if (sr.flipX)            
            {
                Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
            }
            if (!sr.flipX)
            {
                Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
            }

            AudioCtrl.instance.FireBullets(gameObject.transform.position);
        }
    }

    public void MobileMoveLeft()
    {
        leftPressed = true;
    }

    public void MobileMoveRight()
    {
        rightPressed = true;
    }

    public void MobileStop()
    {
        leftPressed = false;
        rightPressed = false;

        StopMoving();
    }

    public void MobileFireBullets()
    {
        FireBullets();
    }

    public void MobileJump()
    {
        Jump();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("LevelOneBoss"))
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);

            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
        }

        if (other.gameObject.CompareTag("BigCoin"))
        {
            GameCtrl.instance.UpdateCoinCount();
            SFXCtrl.instance.ShowBulletSparkle(other.gameObject.transform.position);
            Destroy(other.gameObject);
            GameCtrl.instance.UpdateScore(GameCtrl.Item.BigCoin);

            AudioCtrl.instance.CoinPickup(gameObject.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                if (SFXOn)
                {
                    SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);

                    GameCtrl.instance.UpdateCoinCount();

                    GameCtrl.instance.UpdateScore(GameCtrl.Item.Coin);

                    AudioCtrl.instance.CoinPickup(gameObject.transform.position);
                }
                GameCtrl.instance.UpdateCoinCount();
                break;

            case "Water":

                // show the splash effect
                SFXCtrl.instance.ShowSplash(other.gameObject.transform.position);

                AudioCtrl.instance.WaterSplash(gameObject.transform.position);

                // inform the GameCtrl
                //GameCtrl.instance.PlayerDrowned(other.gameObject);

                break;

            case "Powerup_Bullet":
                canFire = true;

                btn_Fire.SetActive(true);           //this is to make the Fire button appear only after the player collects the key

                SFXCtrl.instance.ShowBulletSparkle(btn_Fire.gameObject.transform.position);

                Vector3 powerupPos = other.gameObject.transform.position;
                AudioCtrl.instance.PowerUp(gameObject.transform.position);
                Destroy(other.gameObject);
                if (SFXOn)
                    SFXCtrl.instance.ShowBulletSparkle(powerupPos);
                break;

            case "Enemy":
                GameCtrl.instance.PlayerDiedAnimation(gameObject);
                AudioCtrl.instance.PlayerDied(gameObject.transform.position);
                break;
            case "BossKey":
                GameCtrl.instance.ShowLever();
                break;
            default:
                break;
        }
    }

    void OnApplicationQuit() 
    {
        GameCtrl.instance.DeleteCheckPoints();

        GameCtrl.instance.data.keyFound[0] = false;
        GameCtrl.instance.data.keyFound[1] = false;
        GameCtrl.instance.data.keyFound[2] = false;

        GameCtrl.instance.data.lives = 3;

        // set the number of coins to 0
        GameCtrl.instance.data.coinCount = 0;

        // set the score to 0
        GameCtrl.instance.data.score = 0;
    }

    /*void DisableCameraFollow()
    {
        Camera.main.GetComponent<CameraCtrl>().enabled = false;
    }*/
}
