using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    public bool isSprinkler;
    public float minTime;
    public float maxTime;
    public float time;
    public GameObject droplet;
    public Vector2 minPos;
    public Vector2 maxPos;
    Vector3 pos;

    private void Start()
    {
        time = Random.Range(minTime, maxTime);
        if(!isSprinkler)
        {
            pos.x = Random.Range(minPos.x, maxPos.x);
            pos.y = Random.Range(minPos.y, maxPos.y);
            pos.z = -1;
        }
        else
        {
            pos.x = 0;
            pos.y = 0;
            pos.z = -1;            
        }
        StartCoroutine(SpawnDroplet());
    }

    public IEnumerator SpawnDroplet()
    {
        Debug.Log("TIMP" + time);
        yield return new WaitForSeconds(time);

        GameObject dropletObject = Instantiate(droplet, pos, Quaternion.identity);

        time = Random.Range(minTime, maxTime);

        if(!isSprinkler)
        {
            pos.x = Random.Range(minPos.x, maxPos.x);
            pos.y = Random.Range(minPos.y, maxPos.y);
            pos.z = -1;
        }
        else
        {
            Destroy(dropletObject.GetComponent<Rigidbody2D>());
            pos.x = 0;
            pos.y = 0;
            pos.z = -1;

            dropletObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2);
        }

        yield return new WaitForSeconds(5);
        if (dropletObject != null)
        {
            Destroy(dropletObject);
        }

        StartCoroutine(SpawnDroplet());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Droplet")
        {
            Destroy(collision.gameObject);
        }
    }
}
