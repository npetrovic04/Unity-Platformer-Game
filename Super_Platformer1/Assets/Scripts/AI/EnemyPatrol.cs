using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// omogucuje jednostavno kretanje izmedju dve pozicije
/// </summary>
public class EnemyPatrol : MonoBehaviour
{
    public Transform leftBound, rightBound;         // the boundaries within which the enemy moves
    public float speed;                             // the speed at which the enemy moves
    public float maxDelay, minDelay;                // random delay before the enemy turns and moves back

    bool canTurn;                                   // checks when the enemy can turn
    float originalSpeed;                            // help turn the enemy around

    Rigidbody2D rb;
    SpriteRenderer sr;                              // helps with flipping player direction
    Animator anim;                                  

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        SetStartingDirection();

        canTurn = true;
    }
	
	void Update ()
    {
        Move();

        FlipOnEdges();
    }
    void Move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }
    void SetStartingDirection()
    {
        if (speed > 0)
            sr.flipX = true;
        else if (speed < 0)
            sr.flipX = false;
    }
    void FlipOnEdges()
    {
        if (sr.flipX && transform.position.x >= rightBound.position.x)
        {
            if (canTurn)
            {
                canTurn = false;
                originalSpeed = speed;
                speed = 0;
                StartCoroutine("TurnLeft", originalSpeed);
            }
        }
        else if (!sr.flipX && transform.position.x <= leftBound.position.x)
        {
            if (canTurn)
            {
                canTurn = false;
                originalSpeed = speed;
                speed = 0;
                StartCoroutine("TurnRight", originalSpeed);
            }
        }
    }
    IEnumerator TurnLeft(float originalSpeed)
    {
        anim.SetBool("IsIdle", true);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        anim.SetBool("IsIdle", false);
        sr.flipX = false;
        speed = -originalSpeed;
        canTurn = true;
    }
    IEnumerator TurnRight(float originalSpeed)
    {
        anim.SetBool("IsIdle", true);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        anim.SetBool("IsIdle", false);
        sr.flipX = true;
        speed = -originalSpeed;
        canTurn = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftBound.position, rightBound.position);
    }
}
