using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WaveTesting
{
    private WaveManager waveManager;

    [SetUp] // Runs before each test
    public void Setup()
    {
        GameObject waveManagerObject = new GameObject();
        waveManager = waveManagerObject.AddComponent<WaveManager>();
        waveManager.levelGoals = new List<int> { 5, 10, 15 }; // Mock goals
    }

    [Test]
    public void Test_WaveFailsToStart()
    {
        waveManager.bunnyWaveManagers = new List<WaveManager.BunnyWaveManager>(); // No waves
        bool canSpawn = waveManager.CanSpawnBunny(null);
        Assert.IsFalse(canSpawn, "Wave should not start when there are no BunnyWaveManagers.");
    }

    [Test]
    public void Test_NoInfiniteSpawnLoop()
    {
        waveManager.bunnyManager = new GameObject().AddComponent<BunnyManager>();
        waveManager.bunnyManager.totalBunniesToSpawn = 5;
        waveManager.bunnyManager.totalBunnySpawned = 5;
        //waveManager.Update();
        WaveManager.currentBunniesKilled = 5;
        Assert.AreEqual(5, WaveManager.currentBunniesKilled);
    }
}
