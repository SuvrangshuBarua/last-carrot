using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Entities/Bunnies", fileName = "New Bunny")]
public class BunnyScriptableObject : ScriptableObject
{
	public GameObject bunnyDefault;
	public float bunnyHealth;
	public float bunnyDamage;
	public float bunnySpeed;
	public float attackInterval;

	public bool overrideDefaultSprite;

	public enum BunnyType
	{
		Default,
		Soldier,
		Fast
	}
}
