using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the camera follow the player
/// </summary>
public class CameraCtrl : MonoBehaviour
{
    public Transform player;
    public float yOffset;

    void Start ()
    {
		
	}
	
	void LateUpdate ()
    {
        // makes the camera follow the player in the x axis
        transform.position = new Vector3(player.position.x, 0.65f, transform.position.z);

        // makes the camera follow the player in the x and y axis
        //transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
    }
}
