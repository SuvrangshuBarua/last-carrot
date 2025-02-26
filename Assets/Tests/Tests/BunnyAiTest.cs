using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BunnyAiTest
{
    private GameObject slowBunny;
    private GameObject fastBunny;
    private GameObject shieldedBunny;

    [SetUp]
    public void Setup()
    {
        slowBunny = new GameObject("SlowBunny");
        fastBunny = new GameObject("FastBunny");
        shieldedBunny = new GameObject("ShieldedBunny");

        // Add necessary components and set initial states for bunnies
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(slowBunny);
        Object.Destroy(fastBunny);
        Object.Destroy(shieldedBunny);
    }

    [Test]
    public void SlowBunnyMovesAtCorrectSpeed()
    {
        // Set speed and path for slow bunny
        // Assert that slow bunny moves at the correct speed and follows the correct path
    }

    [Test]
    public void FastBunnyMovesFasterAndIsWeaker()
    {
        // Set speed and health for fast bunny
        // Assert that fast bunny moves faster than regular bunnies and has less health
    }

    [Test]
    public void ShieldedBunnyTakesMultipleHits()
    {
        // Set health for shielded bunny
        // Assert that shielded bunny takes multiple hits before dying
    }

    [Test]
    public void BunniesDoNotWalkThroughObstacles()
    {
        // Set up obstacles and path for bunnies
        // Assert that bunnies do not walk through obstacles
    }

    [UnityTest]
    public IEnumerator BunniesHandleEdgeCases()
    {
        // Set up scenarios for edge cases (e.g., bunnies getting stuck, multiple bunnies overlapping)
        // Assert that bunnies handle these edge cases correctly
        yield return null;
    }
}
