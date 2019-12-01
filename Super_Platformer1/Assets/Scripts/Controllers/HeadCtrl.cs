using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It checks the collision with breakable game objects and informs SFXCtrl to do what it takes
/// </summary>
public class HeadCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            // informs SFXCtrl to do what it takes
            SFXCtrl.instance.HandleBoxBreaking(other.gameObject.transform.parent.transform.position);

            // sets cat velocity to zero
            gameObject.transform.parent.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            Destroy(other.gameObject.transform.parent.gameObject);
        }
        if (other.gameObject.CompareTag("Invisible"))
        {
            gameObject.transform.parent.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
