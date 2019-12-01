using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys host gameobject with specified delay eg. bullets
/// </summary>
public class DestroyWithDelay : MonoBehaviour
{
    public float delay;     // how long to wait before destroying gameobject

	void Update ()
    {
        Destroy(gameObject, delay);
    }
}
