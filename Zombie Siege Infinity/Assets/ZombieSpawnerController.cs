using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpawnerController : MonoBehaviour
{
    public static int TotalZombiesAlive = 0; // Общее количество зомби
    public static int totalSpawnerCount = 0; // Общее количество спаунеров
    public static List<ZombieSpawnerController> allSpawners = new List<ZombieSpawnerController>();

    public int initialZombiePerWave = 1;
    public int zombiesToSpawnThisWave;

    public int bossZombieCount = 1;

    public float spawnDelay = 0.5f;
    public int currentWave = 0;
    public float waveCooldown = 10.0f;

    public bool isCoolDown;
    public float coolDownCounter = 0;

    public List<Enemy> currentZombiesAlive;

    public GameObject ZombieRegularPrefab;
    public GameObject ZombieBossPrefab;

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI coolDownCounterUI;
    public TextMeshProUGUI currentWaveUI;

    private void Awake()
    {
        totalSpawnerCount++; // Increment total number of spawners when a new spawner is created
        allSpawners.Add(this);
    }

    private void OnDestroy()
    {
        totalSpawnerCount--; // Decrement total number of spawners when a spawner is destroyed
        allSpawners.Remove(this);
    }

    private void Start()
    {
        CalculateZombiesPerWave();
        StartNextWave();
    }

    private void CalculateZombiesPerWave()
    {
        int baseZombiesThisWave = (currentWave > 1) ? initialZombiePerWave + (2 * (currentWave - 1)) : initialZombiePerWave;
        int zombiesPerSpawner = baseZombiesThisWave / totalSpawnerCount;
        int extraZombies = baseZombiesThisWave % totalSpawnerCount;

        foreach (var spawner in allSpawners)
        {
            spawner.zombiesToSpawnThisWave = zombiesPerSpawner + (extraZombies > 0 ? 1 : 0);
            extraZombies--;
        }
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;
        currentWaveUI.text = "Wave: " + currentWave.ToString();
        CalculateZombiesPerWave();
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < zombiesToSpawnThisWave; i++)
        {
            Vector3 spawnPosition = GenerateRandomPosition();
            InstantiateZombie(ZombieRegularPrefab, spawnPosition);
            yield return new WaitForSeconds(spawnDelay);
        }

        if (currentWave % 5 == 0)
        {
            for (int j = 0; j < bossZombieCount; j++)
            {
                Vector3 spawnPosition = GenerateRandomPosition();
                InstantiateZombie(ZombieBossPrefab, spawnPosition);
            }
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f));
        return transform.position + spawnOffset;
    }

    private void InstantiateZombie(GameObject zombiePrefab, Vector3 spawnPosition)
    {
        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = zombie.GetComponent<Enemy>();
        currentZombiesAlive.Add(enemyScript);
        TotalZombiesAlive++;
    }

    private void Update()
    {
        List<Enemy> zombiesToRemove = new List<Enemy>();
        foreach (Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
                TotalZombiesAlive--;
            }
        }

        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        if (TotalZombiesAlive == 0 && !isCoolDown)
        {
            StartCoroutine(StartCoolDown());
        }

        if (isCoolDown)
        {
            coolDownCounter -= Time.deltaTime;
            coolDownCounterUI.text = coolDownCounter.ToString("F2");
        }
        else
        {
            coolDownCounter = waveCooldown;
        }
    }

    private IEnumerator StartCoolDown()
    {   
        isCoolDown = true;
        waveOverUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(waveCooldown);
        isCoolDown = false;
        waveOverUI.gameObject.SetActive(false);
        StartNextWave();
    }
}
