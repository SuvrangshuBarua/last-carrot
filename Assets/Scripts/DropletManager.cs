using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;

public class DropletManager : MonoBehaviour
{
    public List<GameObject> droplets = new List<GameObject>();

    public int maxDroplets = 3;
    public float spawnInterval = 2f;

    private List<GameObject> activeDroplets = new List<GameObject>();
    private float timer;

    [SerializeField] private int collectedResources;
    public int CollectedResources;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnDroplet();
        }

        if(Input.GetMouseButtonDown(0))
        {
            CollectDroplet();
        }
    }
    public void SpawnDroplet()
    {
        if (activeDroplets.Count >= maxDroplets) return;

        objectContainer container = gameManager.instance.GetRandomObjectContainer();
        int dropletType = Random.Range(0, droplets.Count);

        if (container != null)
        {
            
            GameObject newDroplet = Instantiate(droplets[dropletType], container.transform);
            Droplet droplet = newDroplet.GetComponent<Droplet>();
            droplet.AssignContainer(container);
            newDroplet.transform.localPosition = Vector2.zero;
            activeDroplets.Add(newDroplet);
        }
        
    }
    void CollectDroplet()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            GameObject clickedObject = result.gameObject;

            if (activeDroplets.Contains(clickedObject))
            {
                Droplet droplet = clickedObject.GetComponent<Droplet>();
                droplet.UnassignContainer();
                RemoveDroplet(clickedObject);
                collectedResources++;
                Debug.Log($"Resources collected: {collectedResources}");
                break;
            }
        }
    }

    public void RemoveDroplet(GameObject droplet)
    {
        activeDroplets.Remove(droplet);
        Destroy(droplet);
    }

}
