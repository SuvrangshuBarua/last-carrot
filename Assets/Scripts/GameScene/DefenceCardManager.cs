using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class DefenceCardManager : MonoBehaviour
{
    
    [Header("Cards Parameters")]
    public int amountOfCards = 0;
    public DefenceCardScriptableObject[] defenceCardSO; 
    public GameObject cardPrefab;
    public Transform cardHolderTransform;

    [Header("Defence Parameters")]
    public List<GameObject> defenceCards;
    public float cooldown;
    public int cost;
    public Texture defenceIcon;

    public Transform selectionTransform;
    public GameObject selectionCardPrefab;

    public List<int> selectedIndexes;
    public List<GameObject> selectionCards;

    public int minCardAllowed;

    public int maxCardAllowed;
    public Button letsRockButton;

    void Start()
    {
        Debug.Log("Size of defenceCardSO: " + defenceCardSO.Length);
        amountOfCards = defenceCardSO.Length;
        defenceCards = new List<GameObject>();
        // Print the contents of defenceCardSO
        for (int i = 0; i < defenceCardSO.Length; i++)
        {
            if (defenceCardSO[i] != null)
            {
                Debug.Log($"Card {i}: Name = {defenceCardSO[i].name}, Cost = {defenceCardSO[i].cost}, Damage = {defenceCardSO[i].damage}");
            }
            else
            {
                Debug.LogWarning($"Card {i} is null!");
            }
        }// Print the contents of defenceCardSO
        for (int i = 0; i < defenceCardSO.Length; i++)
        {
            if (defenceCardSO[i] != null)
            {
                Debug.Log($"Card {i}: Name = {defenceCardSO[i].name}, Cost = {defenceCardSO[i].cost}, Damage = {defenceCardSO[i].damage}");
            }
            else
            {
                Debug.LogWarning($"Card {i} is null!");
            }
        }
        selectionCards = new List<GameObject>();

        for(int i = 0; i < amountOfCards; i++)
        {
            AddDefenceCardSelection(i);

            Debug.Log(7);
            // if (cardPrefab == null) Debug.LogError("Card Prefab is null!");
            // if (cardHolderTransform == null) Debug.LogError("Card Holder Transform is null!");
            // AddDefenceCard(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        letsRockButton.interactable = defenceCards.Count >= minCardAllowed;
        letsRockButton.interactable = defenceCards.Count <= maxCardAllowed;
    }

    public void AddDefenceReference(DefenceCardScriptableObject defenceSO, CardManager parentCard = default)
    {
        AddDefenceCard(new List<DefenceCardScriptableObject>(defenceCardSO).IndexOf(defenceSO), parentCard);
    }

    public void AddDefenceCard(int index, CardManager parentCard = default)
    {
        if (selectedIndexes.Contains(index))
        {
            int indexPos = selectedIndexes.IndexOf(index);

            GameObject tempRef = defenceCards[indexPos];

            defenceCards.Remove(tempRef);

            Destroy(tempRef);

            selectedIndexes.Remove(index);
        }
        else
        {
            selectedIndexes.Add(index);

            GameObject card = Instantiate(cardPrefab, cardHolderTransform);
            //GameObject card = Instantiate(cardPrefab, cardHolderTransform);
            Animator animator = card.GetComponent<Animator>();

            if (animator == null)
            {
                Debug.LogError("No Animator found on instantiated object (Selection): " + card.name);
            }
            else
            {
                Debug.Log("Animator found on instantiated object (Selection): " + card.name);
            }
            //// ✅ **Trigger animation only if the object has animation**
            //if (defenceCardSO[index].hasAnimation)
            //{
            //    //Animator animator = card.GetComponent<Animator>();
            //    if (animator != null)
            //    {
            //        Debug.Log("Triggering animation for " + defenceCardSO[index].name);
            //        animator.SetTrigger("Activate");
            //        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            //        if (stateInfo.IsName("CatarpultAnimations"))  // Ensure this matches the name of the animation state
            //        {
            //            Debug.Log("Animation is playing.");
            //        }
            //        else
            //        {
            //            Debug.Log("Animation is not playing yet, or transition failed.");
            //        }
            //    }
              
            //}

            CardManager cardManager = card.GetComponent<CardManager>();

            cardManager.defenceCardScriptableObject = defenceCardSO[index];
            cardManager.defenceSprite = defenceCardSO[index].defenceSprite;
            cardManager.UI = GameObject.FindGameObjectWithTag("Canvas");

            defenceCards.Add(card);

            //Getting Variables
            defenceIcon = defenceCardSO[index].defenceIcon;
            cost = defenceCardSO[index].cost;
            cooldown = defenceCardSO[index].cooldown;

            cardManager.parentCard = parentCard;
            cardManager.defenceCardManager = this;

            Debug.Log("Name : " + parentCard.gameObject.name);

            //Updating UI
            card.GetComponentInChildren<RawImage>().texture = defenceIcon;
            card.GetComponentInChildren<TMP_Text>().text = "" + cost;


        }
    }

    private void AddDefenceCardSelection(int index)
    {
        GameObject card = Instantiate(selectionCardPrefab, selectionTransform);
                    Animator animator = card.GetComponent<Animator>();


        Debug.Log(selectionTransform.name);

        CardManager cardManager = card.GetComponent<CardManager>();

        cardManager.defenceCardScriptableObject = defenceCardSO[index];
        cardManager.defenceSprite = defenceCardSO[index].defenceSprite;
        cardManager.UI = GameObject.FindGameObjectWithTag("Canvas");

        selectionCards.Add(card);

        //Getting Variables
        defenceIcon = defenceCardSO[index].defenceIcon;
        cost = defenceCardSO[index].cost;
        cooldown = defenceCardSO[index].cooldown;

        cardManager.defenceCardManager = this;

        //Updating UI
        card.GetComponentInChildren<RawImage>().texture = defenceIcon;
        card.GetComponentInChildren<TMP_Text>().text = "" + cost;
    }
}
