using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DeathManager;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public static DeathManager instance;

    public delegate void OnDeath();
    public static OnDeath onDeath;

    public TitleAnimations deathAnim;

    public bool hasLost;

    public void Awake()
    {
        instance = this;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided death!");

        if (collision.tag == "Bunny" && !hasLost)
        {
            hasLost = true; 

            //Ending Code here
            onDeath?.Invoke();
            StartCoroutine(LevelLost());
            
        }
    }

    public IEnumerator LevelLost()
    {
        deathAnim.ShowTitle();
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(3);
    }
}