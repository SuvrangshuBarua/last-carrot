using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    private objectContainer assignedContainer;

    public void AssignContainer(objectContainer container)
    {
        assignedContainer = container;
        assignedContainer.IsOccupied = true;
    }
    public void UnassignContainer()
    {
        if (assignedContainer != null)
        {
            assignedContainer.IsOccupied = false;
            assignedContainer = null;
        }
    }
}
