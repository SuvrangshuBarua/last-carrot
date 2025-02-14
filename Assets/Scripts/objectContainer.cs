using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class objectContainer : MonoBehaviour
{
    [SerializeField] private bool isOccupied;
    public gameManager manager;
    public Image backgroundImage;

    public bool IsOccupied
    {
        get
        {
            return isOccupied;
        }
        set
        {
            isOccupied = value;
        }
    }

    private void Start()
    {
        manager = gameManager.instance;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (manager.currentContainer == null && manager.draggingObject != null && isOccupied == false)
        {
            manager.currentContainer = this.gameObject;
            backgroundImage.enabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(manager.currentContainer == this.gameObject)
        {
            manager.currentContainer = null;
            backgroundImage.enabled = false;
        }
        
    }
}
