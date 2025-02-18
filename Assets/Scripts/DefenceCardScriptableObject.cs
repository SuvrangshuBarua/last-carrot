using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Defence Cards", fileName = "New Defence Card")]

public class DefenceCardScriptableObject : ScriptableObject
{
    public Texture defenceIcon;
    public int cost;
    public float cooldown;
    public Sprite defenceSprite;

    public Vector2 size;
}
