using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeActivator : MonoBehaviour
{
    public GameObject bee;              // reference to bomber bee

    BomberBeeAI bbai;                   // AI engine bomber bee's


    // Use this for initialization
    void Start ()
    {
        bbai = bee.GetComponent<BomberBeeAI>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bbai.ActivateBee(other.gameObject.transform.position);
        }
    }
}
