using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridSystemTesting
{
    [Test]
    public void ObjectSnapsToCorrectGridPosition()
    {
        var container = new GameObject().AddComponent<objectContainer>();
        container.transform.position = Vector3.zero;
        container.isFull = false;

        var manager = new GameObject().AddComponent<gameManager>();
        gameManager.instance = manager;
        manager.currentContainer = container.gameObject;

        // Creating a mock dragging object
        var draggingPrefab = new GameObject("DraggingObject");
        var draggingObject = Object.Instantiate(draggingPrefab);
        draggingObject.AddComponent<objectDragging>();

        var card = draggingObject.AddComponent<objectCard>();
        var placementPrefab = new GameObject("PlacementPrefab");
        card.objectPlacement = placementPrefab;

        draggingObject.GetComponent<objectDragging>().card = card;

        manager.draggingObject = draggingObject;

        // Act
        manager.placeObject();

        // Assert
        Assert.IsNotNull(manager.currentContainer.transform.GetChild(0), "Object was not placed.");
        Assert.AreEqual(Vector3.zero, manager.currentContainer.transform.GetChild(0).localPosition,
            "Object did not snap to the correct grid position.");
    }

    [Test]
    public void PreventOverlappingPlacements()
    {
        // Creating a Mock Container
        var container = new GameObject().AddComponent<objectContainer>();
        container.isFull = true; // Simulating occupied tile

        // Creating a Mock Manager
        var manager = new GameObject().AddComponent<gameManager>();
        gameManager.instance = manager;
        manager.currentContainer = container.gameObject;

        // Creating a Mock Dragging Object
        var draggingPrefab = new GameObject("DraggingObject");
        var draggingObject = Object.Instantiate(draggingPrefab);
        var objectDragging = draggingObject.AddComponent<objectDragging>();

        var card = draggingObject.AddComponent<objectCard>();
        var placementPrefab = new GameObject("PlacementPrefab");
        card.objectPlacement = placementPrefab;

        objectDragging.card = card;
        manager.draggingObject = draggingObject;

        // Trying to Place Object on Full Tile
        manager.placeObject();

        // Ensuring No Object Was Placed
        Assert.AreEqual(1, container.transform.childCount,
            "Object was placed on an occupied tile.");
    }

    [Test]
    public void DefensesCannotBePlacedOnOccupiedTiles()
    {
        // Creating a Mock Container
        var container = new GameObject("Container").AddComponent<objectContainer>();
        container.isFull = true; // Mark the container as occupied

        // Creating a Mock Manager
        var manager = new GameObject("GameManager").AddComponent<gameManager>();
        gameManager.instance = manager;
        manager.currentContainer = container.gameObject;

        // Creating a Mock Dragging Object
        var draggingPrefab = new GameObject("DraggingObject");
        var draggingObject = Object.Instantiate(draggingPrefab);
        var objectDragging = draggingObject.AddComponent<objectDragging>();

        // Creating a Mock Card
        var card = draggingObject.AddComponent<objectCard>();
        var placementPrefab = new GameObject("DefensePrefab");
        card.objectPlacement = placementPrefab; // Mock the defense object

        // Connecting objectDragging to card
        objectDragging.card = card;

        // Assigning to manager
        manager.draggingObject = draggingObject;

        // Trying to Place Defense
        manager.placeObject();

        // Ensuring No Defense Was Placed
        Assert.AreEqual(1, container.transform.childCount,
            "A defense was placed on an occupied tile.");
    }
}
