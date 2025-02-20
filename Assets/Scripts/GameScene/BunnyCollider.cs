using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyCollider : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bunny")
        {
            Destroy(collision.gameObject);
        }
    }
}
