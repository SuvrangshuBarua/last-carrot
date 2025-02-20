using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class BunnyController : MonoBehaviour
{
    public BunnyScriptableObject thisBunnySO;
    public float speed;
    public float health;
    public float currentHealth;
    public float damage;
    public float attackInterval;
    GameObject target;
    public bool isAttacking;

    public float damageDelay = 2f;
    public bool isFastBunny;

    public AudioClip damageAudio;

    bool isDying;

    bool incremented = false;

    [Header("Animator Parameters")]
    public bool isWalking;


    private void Start()
    {
        speed = thisBunnySO.bunnySpeed;
        health = thisBunnySO.bunnyHealth;
        damage = thisBunnySO.bunnyDamage;
        attackInterval = thisBunnySO.attackInterval;
        currentHealth = health;
    }
    private void Update()
    {
        if (target == null)
        {
            isAttacking = false;
        }

        if (!isAttacking && !isDying)
        {
            this.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            this.transform.position = this.transform.position;
        }


        if (currentHealth <= 0)
        {
            isDying = true;

            if (!incremented)
            {
                incremented = true;
                WaveManager.currentBunniesKilled++;
            }

            Destroy(this.GetComponent<Rigidbody2D>());

            foreach (var item in this.GetComponents<BoxCollider2D>())
            {
                Destroy(item);
            }

            Destroy(this.gameObject, .5f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        //Detect defence collisions
        if (collision.gameObject.tag == "Defence" || collision.gameObject.GetComponent<DefenceManager>() != null)
        {
            isAttacking = true;
            target = collision.gameObject;
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
        isAttacking = false;
    }

    public IEnumerator Attack()
    {
        Debug.Log("Attacking...");
        //isWalking = false;
        //Attack Defence
        if (target != null)
        {
            target.GetComponent<DefenceManager>().Damage(damage);
        }

        yield return new WaitForSeconds(attackInterval);

        if (target != null)
        {
            StartCoroutine(Attack());
        }
    }

    public void DealDamage(float amnt)
    {
        //Audio to play
        this.GetComponent<AudioSource>().PlayOneShot(damageAudio);
        currentHealth -= amnt;

        StartCoroutine(DamageColor(this.gameObject.GetComponent<SpriteRenderer>()));

        foreach (Transform item in this.transform.GetComponentInChildren<Transform>())
        {
            StartCoroutine(DamageColor(item.gameObject.GetComponent<SpriteRenderer>()));   
        }

    }

    public IEnumerator DamageColor(SpriteRenderer spriteRenderer)
    {
        for(int i = 0; i <= 255; i+=10)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(i, i, i);
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 255; i <= 0; i-=10)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(i, i, i);
            }

            yield return new WaitForSeconds(0.1f);
        }

    }
}
