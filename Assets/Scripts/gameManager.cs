using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;

    public static gameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void placeObject()
    {
        if (draggingObject != null && currentContainer != null)
        {
            Instantiate(draggingObject.GetComponent<objectDragging>().card.objectPlacement, currentContainer.transform);
            currentContainer.GetComponent<objectContainer>().isFull = true;
        }
    }
}
