using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It allows the platform to move between two positions
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    public Transform pos_1, pos_2;
    public float speed;
    public Transform startPos;

    Vector3 nextPos;

    // Use this for initialization
    void Start ()
    {
        nextPos = startPos.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position == pos_1.position)
        {
            nextPos = pos_2.position;
        }
        if (transform.position == pos_2.position)
        {
            nextPos = pos_1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos_1.position, pos_2.position);
    }
}
