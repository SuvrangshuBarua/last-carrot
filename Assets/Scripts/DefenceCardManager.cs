using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DefenceCardManager : MonoBehaviour
{
    
    [Header("Cards Parameters")]
    public int amountOfCards = 0;
    public DefenceCardScriptableObject[] defenceCardSO;
    public GameObject cardPrefab;
    public Transform cardHolderTransform;

    [Header("Defence Parameters")]
    public GameObject[] defenceCards;
    public float cooldown;
    public int cost;
    public Texture defenceIcon;

    void Start()
    {
        amountOfCards = defenceCardSO.Length;
        defenceCards = new GameObject[amountOfCards];
        for(int i = 0; i < amountOfCards; i++)
        {
            if (cardPrefab == null) Debug.LogError("Card Prefab is null!");
            if (cardHolderTransform == null) Debug.LogError("Card Holder Transform is null!");
            AddDefenceCard(i);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDefenceCard(int index)
    {
        GameObject card = Instantiate(cardPrefab, cardHolderTransform);
        CardManager cardManager = card.GetComponent<CardManager>();

        cardManager.defenceCardScriptableObject = defenceCardSO[index];
        cardManager.defenceSprite = defenceCardSO[index].defenceSprite;
        cardManager.UI = GameObject.FindGameObjectWithTag("Canvas");

        defenceCards[index] = card;

         //Getting variables
        defenceIcon = defenceCardSO[index].defenceIcon;
        cost = defenceCardSO[index].cost;
        cooldown = defenceCardSO[index].cooldown;

        //Updating UI
        card.GetComponentInChildren<RawImage>().texture = defenceIcon;
        card.GetComponentInChildren<TMP_Text>().text = "" + cost;
    }
}
