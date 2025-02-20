using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropletSystem : MonoBehaviour
{
    public int dropletValue;

    void OnMouseDown()
    {
        GameObject.FindObjectOfType<GameManager>().AddDroplet(dropletValue);
        Destroy(this.gameObject);
    }

}
