using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishList = new List<GameObject>();
    [SerializeField] private FishType fishTypeToSpawn;
    private GameObject selectedFishPrefab;
    [SerializeField] private int maxFish;
    [SerializeField] int spawnedFishCount;

    [Range(1f, 20f)]
    [SerializeField] private float minSpawnTime = 1f;
    [Range(1f, 20f)]
    [SerializeField] private float maxSpawnTime = 20;
    [SerializeField] private float spawnRadius = 2f;
    private float spawnTime;

    private float waitTime;
    private void Start()
    {
        spawnTime = 0;
        SelectFish();
    }

    private void SelectFish()
    {
        for (int i = 0; i < fishList.Count - 1; i++)
        {
            FishAI ai = fishList[i].GetComponent<FishAI>();
            if (ai.fishType == fishTypeToSpawn)
            {
                selectedFishPrefab = fishList[i];
            }
        }
    }

    private void Update()
    {
        if (spawnedFishCount == maxFish) return;
        if (!selectedFishPrefab) return;
        waitTime += Time.deltaTime;
        if(waitTime >= spawnTime)
        {
            waitTime = 0;
            SpawnFish();
        }
    }

    private void SpawnFish()
    {
        float rndX = transform.position.x + Random.insideUnitCircle.x * spawnRadius;
        float rndZ = transform.position.z + Random.insideUnitCircle.y * spawnRadius;
        Vector3 spawnPos = new Vector3(rndX, -2f, rndZ);
        Instantiate(selectedFishPrefab, spawnPos, Quaternion.identity);
        spawnedFishCount++;
    }


    #region Action Methods
    private void OnFishCatched(FishingArea area)
    {
        if (area != this) return;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        spawnedFishCount--;
    }

    private void OnEnable()
    {
        EventManager.OnFishCatched += OnFishCatched;
    }

    private void OnDisable()
    {
        EventManager.OnFishCatched -= OnFishCatched;
    }
    #endregion
}
