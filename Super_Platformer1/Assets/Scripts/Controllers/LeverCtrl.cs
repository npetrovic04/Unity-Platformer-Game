using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Oslobadja Dog-a iz kaveza
/// </summary>
public class LeverCtrl : MonoBehaviour
{
    public GameObject dog;

    public Vector2 jumpSpeed;               // the speed at which the dog jumps out of the cage
    public GameObject[] stairs;             // the stairs through which the dog jumps
    public Sprite leverPulled;              // a picture of a drawn lever

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start ()
    {
        rb = dog.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.AddForce(jumpSpeed);

            foreach (GameObject stair in stairs)
            {
                stair.GetComponent<BoxCollider2D>().enabled = false;
            }

            SFXCtrl.instance.ShowPlayerLanding(gameObject.transform.position);
     
            sr.sprite = leverPulled;

            Invoke("ShowLeverCompleteMenu", 2f);
        }
    }

    void ShowLeverCompleteMenu()
    {
        GameCtrl.instance.LevelComplete();
    }
}
