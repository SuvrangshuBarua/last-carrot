using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DefenceManager: MonoBehaviour
{
    public DefenceCardScriptableObject thisSO;
    public Transform shootPoint;
    public GameObject bullet;
    public float health;
    public float damage;
    public float range;
    public float speed;
    public LayerMask bunnyLayer;
    public float fireRate;

    public bool isTrap;
    public float changeDuration;
    //public GameObject explosion;
    public Sprite changedSprite;
    //public float rate;
    public Sprite[] states;
    public int stateCount;
    public bool isChanged;

    public bool isDragging = true;
    public bool isEgg;
    public float initialHealth;

    public AudioClip damageAudio;

    public List<DefenceObjectVisual> defenceObjectsVisual;

    public GameObject defenceVisual;

    public string dropedName = "Droped";


    private void Start()
    {
        health = thisSO.health;
        damage = thisSO.damage;
        range = thisSO.range;
        speed = thisSO.speed;
        bullet = thisSO.bullet;
        bunnyLayer = thisSO.bunnyLayer;
        fireRate = thisSO.fireRate;
        isEgg = thisSO.isEgg;
        isTrap = thisSO.isTrap;
        changeDuration = thisSO.changeDuration;
        //explosion = thisSO.explosion;
        changedSprite = thisSO.changedSprite;
        states = thisSO.spriteStates;

        initialHealth = health;
        
        if (isTrap || isEgg)
        {
            StartCoroutine(stateUpdate());
            StartCoroutine(blink());
        }

        StartCoroutine(Attack());

    }

    private void Update()
    {
        if (health <= 0)
        {
            //Dead
            this.gameObject.GetComponentInParent<SlotsManagerCollider>().isOccupied = false;
            Destroy(this.gameObject);
        }
    }

     public IEnumerator WaitUntilDroped()
    {
        yield return new WaitUntil(() => !isDragging);

        if (defenceVisual != default)
        {
            defenceVisual.GetComponent<Animator>().SetBool(dropedName, true);
        }

        CardManager.onDragEvent += DisableAllColliders;
    }

    private void OnDestroy()
    {
        CardManager.onDragEvent -= DisableAllColliders;
    }

    void DisableAllColliders(bool disable)
    {
        foreach (var item in GetComponents<Collider2D>())
        {
            item.enabled = !disable;
        }
    }

    public IEnumerator Attack()
    {
        yield return new WaitUntil(() => !isDragging);
        yield return new WaitForSeconds(fireRate);
        if (speed > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.right, range, bunnyLayer) ;
            Debug.DrawRay(shootPoint.position, shootPoint.right, Color.red);
            if (hit)
            {
                if (hit.transform.tag == "Bunny")
                {
                    Debug.Log("Hit Bunny");
                    GameObject bulletObject = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
                    bulletObject.GetComponent<BulletManager>().damage = damage;
                    bulletObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
                }
            }
            StartCoroutine(Attack());
        }
    }

    public IEnumerator stateUpdate()
    {
        isChanged = false;
        yield return new WaitUntil(() => !isDragging);
        if(isTrap)
        {
            yield return new WaitForSeconds(changeDuration);
        }
        isChanged = true;
    }

    public IEnumerator blink()
    {
        yield return new WaitUntil(() => !isDragging);
        Debug.Log(stateCount);
        this.GetComponent<SpriteRenderer>().sprite = states[stateCount];
        if(isTrap)
        {
            yield return new WaitForSeconds(3);
            stateCount = isChanged ? (stateCount == 0 ? 1 : stateCount) : stateCount;
        }
        else if(health <= initialHealth / 2 && isEgg)
        {
            stateCount = isChanged ? (stateCount == 0 ? 1 : stateCount) : stateCount;
        }
        
        //stateCount = isChanged ? stateCount == 2 ? 3 : 2 : stateCount == 1 ? 0 : 1;
        StartCoroutine(blink());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrap && isChanged)
        {
            if (collision.gameObject.tag == "Bunny")
            {
                collision.GetComponent<BunnyController>().DealDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTrap && isChanged)
        {
            if (collision.gameObject.tag == "Bunny")
            {
                //Instantiate(explosion, this.transform);
                Destroy(this.gameObject);
            }
        }
    }

    public void Damage(float amount)
    {
        if(!this.GetComponent<AudioSource>().isPlaying)
        {
            this.GetComponent<AudioSource>().Play();
        }
        
        health -= amount;
    }
}

[System.Serializable]
public class DefenceObjectVisual
{
    public string name;
    public GameObject defenceObject;
}

