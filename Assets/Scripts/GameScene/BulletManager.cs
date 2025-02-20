using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bunny")
        {
            collision.gameObject.GetComponent<BunnyController>().DealDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
