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
            Transform obj = Instantiate(draggingObject.GetComponent<objectDragging>().card.objectPlacement, currentContainer.transform).transform;
            obj.localPosition = Vector3.zero;
            currentContainer.GetComponent<objectContainer>().isFull = true;
        }
    }
}
