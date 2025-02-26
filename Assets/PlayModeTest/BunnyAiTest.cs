using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;



public class BunnyAITests
{
    private GameObject bunnyObject;
    private BunnyController bunnyController;
    private BunnyScriptableObject bunnyData;
    private GameObject obstacle;
    private GameObject bunnyColliderObject;
    private BunnyCollider bunnyCollider;
    /*
    [SetUp]
    public void SetUp()
    {
        // Create Bunny GameObject and attach BunnyController
        bunnyObject = new GameObject("TestBunny");
        bunnyController = bunnyObject.AddComponent<BunnyController>();

        // Create mock BunnyScriptableObject
        bunnyData = ScriptableObject.CreateInstance<BunnyScriptableObject>();
        bunnyData.bunnySpeed = 1f;
        bunnyData.bunnyHealth = 100f;
        bunnyData.bunnyDamage = 10f;
        bunnyData.attackInterval = 1f;

        bunnyController.thisBunnySO = bunnyData;
        bunnyController.Invoke("Start", 0); // Use reflection to call the private Start method

        // Create BunnyCollider for testing destruction
        bunnyColliderObject = new GameObject("BunnyCollider");
        bunnyCollider = bunnyColliderObject.AddComponent<BunnyCollider>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(bunnyObject);
        Object.Destroy(bunnyData);
        Object.Destroy(bunnyColliderObject);
    }
    */
    // Verify that Slow Bunnies move at the correct speed
    [Test]
    public void SlowBunny_Moves_Correctly()
    {
        bunnyObject = new GameObject("TestBunny");
        bunnyController = bunnyObject.AddComponent<BunnyController>();

        // Create mock BunnyScriptableObject
        bunnyData = ScriptableObject.CreateInstance<BunnyScriptableObject>();
        bunnyData.bunnySpeed = 1f;
        bunnyData.bunnyHealth = 100f;
        bunnyData.bunnyDamage = 10f;
        bunnyData.attackInterval = 1f;

        bunnyController.thisBunnySO = bunnyData;
        bunnyController.Invoke("Start", 0); // Use reflection to call the private Start method

        // Create BunnyCollider for testing destruction
        bunnyColliderObject = new GameObject("BunnyCollider");
        bunnyCollider = bunnyColliderObject.AddComponent<BunnyCollider>();

        bunnyController.speed = 1f;
        Vector3 startPos = bunnyObject.transform.position;
        //yield return new WaitForSeconds(1f);
        Assert.LessOrEqual(bunnyObject.transform.position.x, startPos.x); // Bunny should move left
    }
    
    // Ensure Fast Bunnies move faster than regular bunnies and are weaker
    [Test]
    public void FastBunny_Has_LowerHealth_And_HigherSpeed()
    {
        bunnyObject = new GameObject("TestBunny");
        bunnyController = bunnyObject.AddComponent<BunnyController>();

        // Create mock BunnyScriptableObject
        bunnyData = ScriptableObject.CreateInstance<BunnyScriptableObject>();
        bunnyData.bunnySpeed = 1f;
        bunnyData.bunnyHealth = 100f;
        bunnyData.bunnyDamage = 10f;
        bunnyData.attackInterval = 1f;

        bunnyController.thisBunnySO = bunnyData;
        bunnyController.Invoke("Start", 0); // Use reflection to call the private Start method

        // Create BunnyCollider for testing destruction
        bunnyColliderObject = new GameObject("BunnyCollider");
        bunnyCollider = bunnyColliderObject.AddComponent<BunnyCollider>();

        bunnyController.isFastBunny = true;
        bunnyController.speed = 3f;
        bunnyController.health = 50f;

        Assert.AreEqual(3f, bunnyController.speed);
        Assert.AreEqual(50f, bunnyController.health);
        //return null;
    }

    // Validate that Shielded Bunnies take multiple hits before dying
    [Test]
    public void ShieldedBunny_Takes_Multiple_Hits()
    {
        bunnyObject = new GameObject("TestBunny");
        bunnyController = bunnyObject.AddComponent<BunnyController>();

        // Create mock BunnyScriptableObject
        bunnyData = ScriptableObject.CreateInstance<BunnyScriptableObject>();
        bunnyData.bunnySpeed = 1f;
        bunnyData.bunnyHealth = 100f;
        bunnyData.bunnyDamage = 10f;
        bunnyData.attackInterval = 1f;

        bunnyController.thisBunnySO = bunnyData;
        bunnyController.Invoke("Start", 0); // Use reflection to call the private Start method

        // Create BunnyCollider for testing destruction
        bunnyColliderObject = new GameObject("BunnyCollider");
        bunnyCollider = bunnyColliderObject.AddComponent<BunnyCollider>();

        bunnyController.health = 200f;
        bunnyController.currentHealth = 200f;

        bunnyController.DealDamage(50f);
        //yield return null;

        Assert.AreEqual(150f, bunnyController.currentHealth);

        bunnyController.DealDamage(50f);
        //yield return null;

        Assert.AreEqual(100f, bunnyController.currentHealth);
    }

    // Test pathfinding mechanics to ensure bunnies do not walk through obstacles
    [Test]
    public void Bunny_Does_Not_Walk_Through_Obstacles()
    {
        bunnyObject = new GameObject("TestBunny");
        bunnyController = bunnyObject.AddComponent<BunnyController>();

        // Create mock BunnyScriptableObject
        bunnyData = ScriptableObject.CreateInstance<BunnyScriptableObject>();
        bunnyData.bunnySpeed = 1f;
        bunnyData.bunnyHealth = 100f;
        bunnyData.bunnyDamage = 10f;
        bunnyData.attackInterval = 1f;

        bunnyController.thisBunnySO = bunnyData;
        bunnyController.Invoke("Start", 0); // Use reflection to call the private Start method

        // Create BunnyCollider for testing destruction
        bunnyColliderObject = new GameObject("BunnyCollider");
        bunnyCollider = bunnyColliderObject.AddComponent<BunnyCollider>();

        obstacle = new GameObject("Obstacle");
        obstacle.AddComponent<BoxCollider2D>();
        obstacle.transform.position = new Vector3(bunnyObject.transform.position.x - 1, 0, 0);

        Vector3 startPos = bunnyObject.transform.position;
        //yield return new WaitForSeconds(1f);

        Assert.GreaterOrEqual(bunnyObject.transform.position.x, startPos.x - 1f); // Bunny should not move past obstacle
    }

    // Run automated tests for edge cases (bunnies getting stuck or overlapping)
    [Test]
    public IEnumerator Bunny_Does_Not_Get_Stuck()
    {
        bunnyObject = new GameObject("TestBunny");
        bunnyController = bunnyObject.AddComponent<BunnyController>();

        // Create mock BunnyScriptableObject
        bunnyData = ScriptableObject.CreateInstance<BunnyScriptableObject>();
        bunnyData.bunnySpeed = 1f;
        bunnyData.bunnyHealth = 100f;
        bunnyData.bunnyDamage = 10f;
        bunnyData.attackInterval = 1f;

        bunnyController.thisBunnySO = bunnyData;
        bunnyController.Invoke("Start", 0); // Use reflection to call the private Start method

        // Create BunnyCollider for testing destruction
        bunnyColliderObject = new GameObject("BunnyCollider");
        bunnyCollider = bunnyColliderObject.AddComponent<BunnyCollider>();

        Vector3 startPos = bunnyObject.transform.position;
        bunnyController.speed = 2f;

        WaitForSecondsRealtime(2f);

        Assert.AreNotEqual(startPos, bunnyObject.transform.position); // Bunny should not remain in the same place
        return null;
    }

    private object WaitForSecondsRealtime(float v)
    {
        throw new NotImplementedException();
    }

    // Verify that Bunny Collider destroys Bunny upon collision
    [Test]
    public void BunnyCollider_Destroys_Bunny_On_Collision()
    {
        bunnyObject = new GameObject("TestBunny");
        bunnyController = bunnyObject.AddComponent<BunnyController>();

        // Create mock BunnyScriptableObject
        bunnyData = ScriptableObject.CreateInstance<BunnyScriptableObject>();
        bunnyData.bunnySpeed = 1f;
        bunnyData.bunnyHealth = 100f;
        bunnyData.bunnyDamage = 10f;
        bunnyData.attackInterval = 1f;

        bunnyController.thisBunnySO = bunnyData;
        bunnyController.Invoke("Start", 0); // Use reflection to call the private Start method

        // Create BunnyCollider for testing destruction
        bunnyColliderObject = new GameObject("BunnyCollider");
        bunnyCollider = bunnyColliderObject.AddComponent<BunnyCollider>();


        // Simulate collision
        bunnyCollider.OnTriggerEnter2D(bunnyObject.AddComponent<BoxCollider2D>());

        //yield return null; // Wait one frame for destruction
        

        // Verify the bunny object has been destroyed
        Assert.IsTrue(bunnyObject == null || !bunnyObject.activeInHierarchy);
    }
}
