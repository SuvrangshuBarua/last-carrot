using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnyManager : MonoBehaviour
{
    public BunnyScriptableObject[] bunnyScriptableObjects;
    public float timeInterval;

    public Transform[] columns;

    public List<GameStages> gameStages;

    [SerializeField] List<BunnyScriptableObject> allowedBunnies;

    public int totalBunnySpawned;
    public float waitUntilSpawnWave;

    public int totalBunniesToSpawn;

    public Slider levelProgress;

    private void Awake()
    {
        DeathManager.onDeath += StopSpawningBunnies;
        WaveManager.onWaveTriggered += OnWave;
        levelProgress.maxValue = totalBunniesToSpawn;
    }

    private void OnDestroy()
    {
        DeathManager.onDeath -= StopSpawningBunnies;
        WaveManager.onWaveTriggered -= OnWave;
    }

    public void SpawnBunnies ()
    {
        totalBunnySpawned = 0;

        ReCheckBunnies();
        StartCoroutine(BunnySpawn());
    }

    public void OnWave(int index)
    {
        waitUntilSpawnWave = 1f;
        ReCheckBunnies();
    }

    public void StopSpawningBunnies()
    {
        StopAllCoroutines();    
    }

    [ContextMenu("spawn PV")]

    public IEnumerator BunnySpawn()
    {
        GameStages thisStage = GetCurrentStage();

        timeInterval = thisStage.TimeToWait();

        while (CardManager.isGameStart)
        {
            yield return new WaitForSeconds(timeInterval);

            yield return new WaitForSeconds(waitUntilSpawnWave);
            waitUntilSpawnWave = 0;

            thisStage = GetCurrentStage();

            int amountToSpawn = Random.Range(1, thisStage.maxBunnyPerGroup + 1);

            for (int i = 0; i < amountToSpawn; i++)
            {
                SpawnBunny(allowedBunnies);

                yield return new WaitForSeconds(0.05f);
            }

            timeInterval = thisStage.TimeToWait();
        }
    }

    void ReCheckBunnies()
    {
        Debug.Log("Recheck");
        allowedBunnies = new List<BunnyScriptableObject>();

        foreach (var selectedSO in bunnyScriptableObjects)
        {
            if (WaveManager.instance.CanSpawnBunny(selectedSO))
            {
                Debug.Log(selectedSO.name);
                allowedBunnies.Add(selectedSO);
            }
        }
    }

    public GameStages GetCurrentStage()
    {
        int index = 0;

        for (int i = 0; i < gameStages.Count; i++)
        {
            if (gameStages[i].minBunnyInStage <= totalBunnySpawned)
            {
                index = i;
            }
        }

        return gameStages[index];
    }

    void SpawnBunny(List<BunnyScriptableObject> bunnyScriptableObjects)
    {
        if (bunnyScriptableObjects.Count <= 0)
        {
            Debug.Log("Recheck neeed");
            return;
        }

        //Choose bunny
        BunnyScriptableObject selectedSO = bunnyScriptableObjects[Random.Range(0, bunnyScriptableObjects.Count)];

        //Spawn bunnies
        int columnID = Random.Range(0, columns.Length);
        GameObject bunny = Instantiate(selectedSO.bunnyDefault, columns[columnID]);

        bunny.GetComponent<BunnyController>().thisBunnySO = selectedSO;

        bunny.transform.SetParent(columns[columnID]);
        bunny.transform.position = new Vector3(0, 0, -1);
        bunny.transform.localPosition = new Vector3(0, 0, -1);

        totalBunnySpawned++;

        levelProgress.value = totalBunnySpawned;

        if (totalBunnySpawned == totalBunniesToSpawn)
        {
            StopSpawningBunnies();
        }
    }
}

[System.Serializable]
public class GameStages
{
    public string name;
    public string description;

    public int minBunnyInStage;

    [Range(1, 10)]
    public int maxBunnyPerGroup = 1;

    public Vector2 minMaxTimeDelay;

    public float TimeToWait()
    {
        return Random.Range(minMaxTimeDelay.x, minMaxTimeDelay.y);
    }
}
