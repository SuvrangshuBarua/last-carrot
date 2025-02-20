using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text dropletDisplay;
    public int dropletAmount = 0;
    public int startingDropletAmount;

    public GameObject decorativeBunnies;

    public Transform cardSlotsHolder;

    public BunnyManager bunnyManager;
    public Animator cameraPan;

    public TitleAnimations waveText;

    private void Awake()
    {
        instance = this;

        WaveManager.onWaveTriggered += waveText.OnWave;
        DeathManager.onDeath += EndMatch;
    }

    private void OnDestroy()
    {
        WaveManager.onWaveTriggered -= waveText.OnWave;
        DeathManager.onDeath -= EndMatch;
    }

    private void Start()
    {
        CardManager.isGameStart = false;

        AddDroplet(startingDropletAmount);
    }

    public void StartMatch()
    {
        cameraPan.SetTrigger("PanToPlants");
        CardManager.isGameStart = true;
        RefreshAllDefenceCards();
        bunnyManager.SpawnBunnies();

        DeathManager.instance.hasLost = false;
    }

    public void EndMatch()
    {
        cameraPan.SetTrigger("PanToHouse");
        CardManager.isGameStart = false;
    }


    public void AddDroplet(int amount)
    {
        dropletAmount += amount;
        dropletDisplay.text = "" + dropletAmount;
    }

    public void DeductDroplet(int amount)
    {
        dropletAmount -= amount;
        dropletDisplay.text = "" + dropletAmount;
    }

    public void RefreshAllDefenceCards()
	{
        foreach (Transform card in cardSlotsHolder)
        {
            try
            {
                card.GetComponent<CardManager>().StartRefresh();
            }
            catch (System.Exception)
            {
                Debug.LogError("Card does not contain CardManager script!");
            }
        }
    }

}
