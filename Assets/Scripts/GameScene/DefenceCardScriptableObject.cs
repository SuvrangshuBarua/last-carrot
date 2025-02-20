using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Defence Cards", fileName = "New Defence Card")]

public class DefenceCardScriptableObject : ScriptableObject
{
    public Texture defenceIcon;
    public GameObject bullet;
    public Sprite defenceSprite;
    public LayerMask bunnyLayer;
    public int cost;
    public float cooldown;
    public bool isSprinkler;
    public DropletSpawner dropletSpawnerTemplate;
    public float health;
    public float damage;
    public float range;
    public float speed;
    public float fireRate; 
    [Header("Egg Parameters")]
    public bool isEgg;
    public Sprite damagedEgg;
    [Header("Trap Parameters")]
    public bool isTrap;
    //public GameObject explosion;
    public float changeDuration;
    public Sprite changedSprite;
    [Header("States: 0 - initial, 1 - final")]
    public Sprite[] spriteStates; 

    public Vector2 size;

    public Vector2 colliderSize;
    public float radius;

    public string name;
    public int amountOfBullet = 1;
    public float rateBetweenBullets = 0.25f;

// #nullable enable
//     public DefenceCardScriptableObject? placedOn;
//     public bool placeOnOtherDefence;
// #nullable enable
}
