using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowd : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int crowdAmount;
    [SerializeField] private int radius;
    [SerializeField] private int followDistance;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float deadZoneXMin;
    [SerializeField] private float deadZoneXMax;

    private GameManager gameManager;
    private Player player;

    private void Start()
    {
        StartCoroutine(SpawnCrowd());
        transform.position = new Vector3(0,transform.position.y, player.transform.position.z - followDistance);
    }

    private void Update()
    {
        ChasePlayer();
    }

    private IEnumerator SpawnCrowd()
    {
        for (int i = 0; i <= crowdAmount; i++)
        {
            Vector3 spawnPos = new Vector3(Random.insideUnitSphere.x * radius, 0.25f, Random.insideUnitSphere.z * radius);
            GameObject zombie = Instantiate(enemyPrefab, transform.position + spawnPos, Quaternion.identity);
            zombie.transform.parent = transform;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void ChasePlayer()
    {
        if (gameManager.gameState == GameState.IN_MENU || gameManager.gameState == GameState.PAUSED) return;

        var playerPos = player.transform.position;

        if (playerPos.x < deadZoneXMax && playerPos.x > deadZoneXMin)
        {
            Vector3 followPos = new Vector3(playerPos.x, transform.position.y, playerPos.z - followDistance);
            transform.position = Vector3.Lerp(transform.position, followPos, lerpSpeed);
        }
        else
        {
            Vector3 followPos = new Vector3(transform.position.x, transform.position.y, playerPos.z - followDistance);
            transform.position = Vector3.Lerp(transform.position, followPos, lerpSpeed);
        }
    }

    #region Action Methods
    private void OnGameManagerCreated(GameManager x) => gameManager = x;
    private void OnPlayerCreated(Player x) => player = x;

    private void OnEnable()
    {
        EventManager.OnGameManagerCreated += OnGameManagerCreated;
        EventManager.OnPlayerCreated += OnPlayerCreated;
    }

    private void OnDisable()
    {
        EventManager.OnGameManagerCreated -= OnGameManagerCreated;
        EventManager.OnPlayerCreated -= OnPlayerCreated;
    }
    #endregion
}
