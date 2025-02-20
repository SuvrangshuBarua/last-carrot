using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    
    public Slider musicSlider;
    void Start()
    {
        musicSlider.value = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>().volume;
    }


    // Update is called once per frame
    void Update()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>().volume = musicSlider.value;
    }

    public void GoBack()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
