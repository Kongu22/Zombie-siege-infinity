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

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the list of zombies alive
        CalculateZombiesPerWave();
        StartNextWave();
    }

    private void CalculateZombiesPerWave()
    {
        // Calculate the number of zombies to spawn this wave
        int baseZombiesThisWave = (currentWave > 1) ? initialZombiePerWave + (2 * (currentWave - 1)) : initialZombiePerWave;
        int zombiesPerSpawner = baseZombiesThisWave / totalSpawnerCount;
        int extraZombies = baseZombiesThisWave % totalSpawnerCount;

        // Assign the number of zombies to spawn to each spawner
        foreach (var spawner in allSpawners)
        {
            spawner.zombiesToSpawnThisWave = zombiesPerSpawner + (extraZombies > 0 ? 1 : 0);
            extraZombies--;
        }
    }


    // Start the next wave
    private void StartNextWave()
    {
        // Clear the list of zombies alive
        currentZombiesAlive.Clear();
        currentWave++;
        currentWaveUI.text = "Wave: " + currentWave.ToString();
        CalculateZombiesPerWave();
        StartCoroutine(SpawnWave());
    }

    // Spawn a wave of zombies
    private IEnumerator SpawnWave()
    {
        // Spawn the zombies
        for (int i = 0; i < zombiesToSpawnThisWave; i++)
        {
            Vector3 spawnPosition = GenerateRandomPosition();
            InstantiateZombie(ZombieRegularPrefab, spawnPosition);
            yield return new WaitForSeconds(spawnDelay);
        }
        // Spawn the boss zombie
        if (currentWave % 5 == 0)
        {
            for (int j = 0; j < bossZombieCount; j++)
            {
                Vector3 spawnPosition = GenerateRandomPosition();
                InstantiateZombie(ZombieBossPrefab, spawnPosition);
            }
        }
    }

    // Generate a random position around the spawner
    private Vector3 GenerateRandomPosition()
    {
        // Generate a random position around the spawner
        Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f));
        return transform.position + spawnOffset;
    }

    // Instantiate a zombie at the specified position
    private void InstantiateZombie(GameObject zombiePrefab, Vector3 spawnPosition)
    {
        // Instantiate the zombie
        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = zombie.GetComponent<Enemy>();
        currentZombiesAlive.Add(enemyScript);
        TotalZombiesAlive++;
    }

    private void Update()
    {
        // Check if all zombies are dead
        List<Enemy> zombiesToRemove = new List<Enemy>();
        foreach (Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
                TotalZombiesAlive--;
            }
        }
        // Remove the dead zombies from the list
        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }
        // Check if all zombies are dead and start the cooldown
        if (TotalZombiesAlive == 0 && !isCoolDown)
        {
            StartCoroutine(StartCoolDown());
        }
        // Update the cooldown counter
        if (isCoolDown)
        {
            coolDownCounter -= Time.deltaTime;
            coolDownCounterUI.text = coolDownCounter.ToString("F2");
        }
        else
        {
            // Reset the cooldown counter
            coolDownCounter = waveCooldown;
        }
    }

    // Start the cooldown
    private IEnumerator StartCoolDown()
    {   
        // Start the cooldown
        isCoolDown = true;
        waveOverUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(waveCooldown);
        isCoolDown = false;
        waveOverUI.gameObject.SetActive(false);
        StartNextWave();
    }
}
