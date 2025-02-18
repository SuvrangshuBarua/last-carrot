using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public GameObject UI;
    public SlotsManagerCollider colliderName;
    SlotsManagerCollider prevName;
    public DefenceCardScriptableObject defenceCardScriptableObject;
    public Sprite defenceSprite;
    public GameObject defencePrefab;
    public bool isOverCollider = false;
    GameObject defence;

    public void OnDrag(PointerEventData eventData)
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 pos = new Vector3(0, 0, -1);
        defence = Instantiate(defencePrefab, pos, Quaternion.identity);  
        defence.transform.localScale = defenceCardScriptableObject.size; 
        defence.GetComponent<SpriteRenderer>().sprite = defenceSprite;
        defence.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (colliderName != null && !colliderName.isOccupied)
        {
            colliderName.isOccupied = true;
            defence.tag = "Untagged";
            defence.transform.SetParent(colliderName.transform);
            defence.transform.position = new Vector3(0, 0, -1);
            defence.transform.localPosition = new Vector3(0, 0, -1);
        }
        else
        {
            Destroy(defence);
        }
    }

}
