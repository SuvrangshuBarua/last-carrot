using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class objectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public GameObject objectDrag;
    public GameObject objectPlacement;
    public Canvas canvas;

    private GameObject objectDragInstance;
    private gameManager manager;

    private void Start()
    {
        manager = gameManager.instance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        objectDragInstance.transform.position = Input.mousePosition;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectDragInstance = Instantiate(objectDrag, canvas.transform);
        objectDragInstance.transform.position = Input.mousePosition;
        objectDragInstance.GetComponent<objectDragging>().card = this; 
        manager.draggingObject = objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        manager.placeObject();
        manager.draggingObject = null;
        Destroy(objectDragInstance);
    }
}
