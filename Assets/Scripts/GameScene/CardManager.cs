using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public delegate void OnDragEvent(bool begin);
    public static OnDragEvent onDragEvent;
    public GameObject UI;
    public SlotsManagerCollider colliderName;
    SlotsManagerCollider prevName;
    public DefenceCardScriptableObject defenceCardScriptableObject;
    public Sprite defenceSprite;
    public GameObject defencePrefab;
    public bool isOverCollider = false;
    GameObject defence;
    bool isHoldingDefence;
    public bool isCoolingDown;

    [Tooltip("X: Max Height, Y: Min Height")]
    public Vector2 height;
    public Image refreshImage;

    public bool isSelection;
    public bool isSelected;

    public static bool isGameStart = false;

    public DefenceCardManager defenceCardManager;

    public CardManager parentCard;

    public void OnDrag(PointerEventData eventData)
    {
        if(isSelection)
        {
            return;
        }
        
        if(isCoolingDown)
        {
            return;
        }

        if(isHoldingDefence)
        {
            defence.GetComponent<SpriteRenderer>().sprite = defenceSprite;

            if (prevName != colliderName || prevName == null)
            {
                if(!colliderName.isOccupied)
                {
                    defence.transform.position = new Vector3(0, 0, -1);
                    defence.transform.localPosition = new Vector3(0, 0, -1);
                    isOverCollider = false;

                    if (prevName != null)
                    {
                        prevName.defence = null;
                    }
                    prevName = colliderName;
                }
            }
            else
            {
                if(!colliderName.isOccupied)
                {
                    defence.transform.position = new Vector3(0, 0, -1);
                    defence.transform.localPosition = new Vector3(0, 0, -1);
                }
            }

            if (!isOverCollider) 
            {
                defence.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSelection)
        {
            isSelected = true;
            defenceCardManager.AddDefenceReference(defenceCardScriptableObject, this.gameObject.GetComponent<CardManager>());
        }
        else
        {
            if(!isGameStart)
            {
                parentCard.isSelected = isSelected = false;
                defenceCardManager.AddDefenceReference(defenceCardScriptableObject);
            }
            else
            {

                if(isCoolingDown)
                {
                    return;
                }

                if (GameObject.FindObjectOfType<GameManager>().dropletAmount >= defenceCardScriptableObject.cost)
                {
                    isHoldingDefence = true;
                    Vector3 pos = new Vector3(0, 0, -1);
                    defence = Instantiate(defencePrefab, pos, Quaternion.identity);  
                    defence.GetComponent<DefenceManager>().thisSO = defenceCardScriptableObject;
                    defence.GetComponent<DefenceManager>().isDragging = true;
                    defence.transform.localScale = defenceCardScriptableObject.size; 
                    defence.GetComponent<SpriteRenderer>().sprite = defenceSprite;

                    defence.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    Debug.Log("Not enough money");
                }
            }
        }

        onDragEvent?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isSelection)
        {
            return;
        }

        if(isCoolingDown)
        {
            return;
        }

        if (isHoldingDefence)
        {
            if (colliderName != null && !colliderName.isOccupied) //&& !defenceCardScriptableObject.placeOnOtherDefence)
            {
                PlaceDefence();
            }
            else
            {
                isHoldingDefence = false;
                Destroy(defence);
            }
        }

        onDragEvent?.Invoke(true);
    }

    void PlaceDefence()
    {
        GameObject.FindObjectOfType<GameManager>().DeductDroplet(defenceCardScriptableObject.cost);
        isHoldingDefence = false;
        colliderName.isOccupied = true;
        defence.tag = "Untagged";
        defence.transform.SetParent(colliderName.transform);
        defence.transform.position = new Vector3(0, 0, -1);
        defence.transform.localPosition = new Vector3(0, 0, -1);
        defence.AddComponent<BoxCollider2D> ();
        defence.GetComponent<BoxCollider2D> ().size = new Vector2(1, 1);
        defence.AddComponent<CircleCollider2D> ();
        defence.GetComponent<CircleCollider2D> ().isTrigger = true;
        defence.GetComponent<CircleCollider2D> ().radius = 0.7f;

        defence.GetComponent<DefenceManager>().isDragging = false;

        //Trigger the animation here
       Animator animator = defence.GetComponent<Animator>();
        if (defenceCardScriptableObject.hasAnimation)
        {
            Debug.Log("Triggering animation for placed defence: " + defenceCardScriptableObject.name);
            animator.SetBool("Activate", true);

        }
        else {

            animator.SetBool("Activate", false);

        }

        if (defenceCardScriptableObject.isSprinkler)
        {
            DropletSpawner dropletSpawner = defence.AddComponent<DropletSpawner>();
            dropletSpawner.isSprinkler = true;
            dropletSpawner.minTime = defenceCardScriptableObject.dropletSpawnerTemplate.minTime;
            dropletSpawner.maxTime = defenceCardScriptableObject.dropletSpawnerTemplate.maxTime;
            dropletSpawner.droplet = defenceCardScriptableObject.dropletSpawnerTemplate.droplet;
        }

        //Disable defence before cooldown has finished
        StartCoroutine(cardCoolDown(defenceCardScriptableObject.cooldown));
    }

    public IEnumerator cardCoolDown(float cooldownDuration)
    {
        isCoolingDown = true;

        for (float i = height.x; i <= height.y; i++)
        {
            refreshImage.rectTransform.anchoredPosition = new Vector3(0,i,0);
            yield return new WaitForSeconds (cooldownDuration / height.y);
        }

        isCoolingDown = false;
    }

    public void StartRefresh()
    {
        StartCoroutine(cardCoolDown(defenceCardScriptableObject.cooldown));
    }

}
