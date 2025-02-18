using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManagerCollider : MonoBehaviour
{
    public GameObject defence;
    public bool isOccupied = false;
    void OnMouseOver()
    {
        foreach (CardManager item in GameObject.FindObjectsOfType<CardManager>())
        {
            item.colliderName = this.GetComponent<SlotsManagerCollider>();
            item.isOverCollider = true;
        }

        if (defence == null)
        {
            if (GameObject.FindGameObjectWithTag("Defence") != null)
            {
                defence = GameObject.FindGameObjectWithTag("Defence");
                defence.transform.SetParent(this.transform);
                Vector3 pos = new Vector3(0, 0, -1);
                defence.transform.localPosition = pos;
            }
        }
    }

    private void OnMouseExit()
    {
        //Destroy(defence);
    }
}
