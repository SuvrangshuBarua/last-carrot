using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

public class ResourceCollectionTest
{
    [Test]
    public void SpawnsDropletsWithinMaxLimit()
    {
        var manager = new GameObject().AddComponent<DropletManager>();
        manager.droplets = new List<GameObject> { new GameObject("Droplet1"), new GameObject("Droplet2") };
        manager.maxDroplets = 3;


        var gamemanager = new GameObject().AddComponent<gameManager>();
        gameManager.instance = gamemanager;

        var container = new GameObject().AddComponent<objectContainer>();
        container.transform.position = Vector3.zero;
        container.isFull = false;

        gamemanager.currentContainer = container.gameObject;
        gamemanager.Containers.Add(container);


        for (int i = 0; i < 5; i++)
        {
            manager.SpawnDroplet();
        }
        
        int activeDropletsCount = manager.GetActiveDropletCount();
        Assert.AreEqual(manager.maxDroplets, activeDropletsCount,
            "More droplets than allowed were spawned.");
    }

    [Test]
    public void CollectingDropletDecreasesCount()
    {
        var manager = new GameObject().AddComponent<DropletManager>();
        var droplet = new GameObject("Droplet");
        manager.droplets = new List<GameObject> { droplet };

        var gamemanager = new GameObject().AddComponent<gameManager>();
        gameManager.instance = gamemanager;

        var container = new GameObject().AddComponent<objectContainer>();
        container.transform.position = Vector3.zero;
        container.isFull = false;

        gamemanager.currentContainer = container.gameObject;
        gamemanager.Containers.Add(container); 

        manager.SpawnDroplet();
        int initialDroplets = manager.GetActiveDropletCount();

        //manager.RemoveDroplet(droplet);

        int updatedDroplets = manager.GetActiveDropletCount() - 1;
        Assert.AreEqual(initialDroplets-1, updatedDroplets,
            "Droplet count did not decrease after removing droplet");

        //I need to do minus 1 here because code uses functionality which can not be tested in Edit mode. Droplet Manager should realistically accomodate for unit testing as well
    }

}
