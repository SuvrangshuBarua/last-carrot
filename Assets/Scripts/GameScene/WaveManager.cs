using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public delegate void OnWaveTriggered(int waveIndex);
	public static OnWaveTriggered onWaveTriggered;

    public int currentWave;

    public List<int> levelGoals;

    public Transform markerTransform;
    public GameObject markerPrefab;

    public static int currentBunniesKilled = 0;
    [SerializeField] int currentKills = 0;

    public List<BunnyWaveManager> bunnyWaveManagers;

    int nextGoal = 0;

    public Dictionary<int, int> goalsToIndex;

    public List<MarkersManager> markers;

    int index = 0;
    public float lastKillCount = 0;
    public BunnyManager bunnyManager;
    public GameObject levelWonText;


    private void Awake()
    {
        instance = this;
        goalsToIndex = new Dictionary<int, int>();
        markers = new List<MarkersManager>();
    }

    public bool CanSpawnBunny(BunnyScriptableObject bunny)
	{
		foreach (var item in bunnyWaveManagers)
		{
			if (item.CanSpawn(bunny, currentWave))
			{
				return true;
			}
		}

		return false;
	}

    public void Start()
    {
        int i = 0;

        nextGoal = levelGoals[0];
        currentBunniesKilled = 0;

        foreach (var item in levelGoals)
        {
            GameObject tempMarker = Instantiate(markerPrefab, markerTransform);

            markers.Add(tempMarker.GetComponent<MarkersManager>());

            goalsToIndex.Add(item, i);

            i++;
        }
    }

    private void Update()
    {
        currentKills = currentBunniesKilled;

        if(currentKills == bunnyManager.totalBunniesToSpawn)
        {
            StartCoroutine(LevelWon());
        }

        if (bunnyManager.totalBunnySpawned >= nextGoal && index < markers.Count)
        {
            markers[index].Expand();

            nextGoal = levelGoals[levelGoals.IndexOf(nextGoal) + 1 < levelGoals.Count ? levelGoals.IndexOf(nextGoal) + 1 : 0];

            onWaveTriggered?.Invoke(levelGoals.IndexOf(nextGoal) - 1);
			lastKillCount = nextGoal;
			index++;
			currentWave++;
        }

    }

    public IEnumerator LevelWon()
    {
        levelWonText.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(3);
    }

    [System.Serializable]
    public class BunnyWaveManager
    {
        public List<BunnyScriptableObject> bunnies;
        public List<int> allowedWaves;

        public bool CanSpawn(BunnyScriptableObject bunny, int currentWave)
        {
            if (bunnies == default || allowedWaves == default)
            {
                Debug.LogError("Array is empty");
                return false;
            }

            return bunnies.Contains(bunny) && allowedWaves.Contains(currentWave);
        }
    }

}
