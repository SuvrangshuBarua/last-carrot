using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;

    public List<objectContainer> Containers = new List<objectContainer>();

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
            currentContainer.GetComponent<objectContainer>().IsOccupied = true;
        }
    }

    public objectContainer GetRandomObjectContainer()
    {
        var availableContainers = Containers.Where(c => !c.IsOccupied).ToList();

        if (availableContainers.Count == 0)
            return null;

        return availableContainers[Random.Range(0, availableContainers.Count)];
    }
}
