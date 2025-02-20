using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject defenceSlot;
    [SerializeField] GameObject musicSlider;
    [SerializeField] GameObject SFXSlider;
    [SerializeField] GameObject defencePrefab;
    [SerializeField] GameObject defaultBunnyPrefab;
    [SerializeField] GameObject soldierBunnyPrefab;
    [SerializeField] GameObject fastBunnyPrefab;
    [SerializeField] GameObject selectDefences;
    public bool isActive;

    private void Start()
    {
        musicSlider.GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>().volume;

        SFXSlider.GetComponent<Slider>().value = defencePrefab.GetComponent<AudioSource>().volume;

    }

    private void Update()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>().volume = musicSlider.GetComponent<Slider>().value;

        defencePrefab.GetComponent<AudioSource>().volume = SFXSlider.GetComponent<Slider>().value;
        defaultBunnyPrefab.GetComponent<AudioSource>().volume = SFXSlider.GetComponent<Slider>().value;
        soldierBunnyPrefab.GetComponent<AudioSource>().volume = SFXSlider.GetComponent<Slider>().value;
        fastBunnyPrefab.GetComponent<AudioSource>().volume = SFXSlider.GetComponent<Slider>().value;

        foreach (var defence in GameObject.FindGameObjectsWithTag("Defence"))
        {
            defence.GetComponent<AudioSource>().volume = SFXSlider.GetComponent<Slider>().value;
        }

        foreach (var bunny in GameObject.FindGameObjectsWithTag("Bunny"))
        {
            bunny.GetComponent<AudioSource>().volume = SFXSlider.GetComponent<Slider>().value;
        }
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        defenceSlot.SetActive(false);
        if(selectDefences.activeSelf)
        {
            isActive = true;
            selectDefences.SetActive(false);
        }
        
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        defenceSlot.SetActive(true);
        if(isActive)
        {
            selectDefences.SetActive(true);
            isActive = false;
        }
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
