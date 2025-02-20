using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider SFXslider;
    public bool hasSFXvalue;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Level1()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void Level2()
    {
        SceneManager.LoadSceneAsync(5);
    }

    public void Level3()
    {
        SceneManager.LoadSceneAsync(6);
    }

    public void Home()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void OptionsScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        //SFXslider = GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>();
        //Debug.Log(SFXslider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
