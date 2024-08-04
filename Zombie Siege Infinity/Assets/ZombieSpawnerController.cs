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
        GlobalReferences.Instance.WaveNumber = currentWave;
        StartNextWave();
    }

    private void CalculateZombiesPerWave()
    {
        // Calculate total number of zombies for the current wave based on the wave number
        int totalZombiesForWave = initialZombiePerWave;
        if (currentWave > 1)
        {
            totalZombiesForWave += 2 * (currentWave - 1);
        }

        // Calculate how many zombies each spawner should spawn
        int baseZombiesPerSpawner = totalZombiesForWave / totalSpawnerCount;
        int remainingZombies = totalZombiesForWave % totalSpawnerCount; // Zombies left after even distribution

        // Distribute zombies to each spawner, with some spawners potentially getting an extra zombie to handle remainders
        foreach (var spawner in allSpawners)
        {
            spawner.zombiesToSpawnThisWave = baseZombiesPerSpawner + (remainingZombies > 0 ? 1 : 0);
            remainingZombies--; // Decrease the count of remaining zombies
        }
    }

    // Start the next wave
    private void StartNextWave()
    {
        // Clear the list of zombies alive
        currentZombiesAlive.Clear();
        currentWave++;
        GlobalReferences.Instance.WaveNumber = currentWave;
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
            InstantiateZombie(ZombieRegularPrefab, spawnPosition, false);
            yield return new WaitForSeconds(spawnDelay);
        }
        // Spawn the boss zombie
        if (currentWave % 5 == 0)
        {
            for (int j = 0; j < bossZombieCount; j++)
            {
                Vector3 spawnPosition = GenerateRandomPosition();
                InstantiateZombie(ZombieBossPrefab, spawnPosition, true);
            }
        }
    }

    // Generate a random position around the spawner
    private Vector3 GenerateRandomPosition()
    {
        // Generate a random position around the spawner
        Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-20f, 40f), 5f, UnityEngine.Random.Range(-15f, 10f));
        return transform.position + spawnOffset;
    }

    // Instantiate a zombie at the specified position
    private void InstantiateZombie(GameObject zombiePrefab, Vector3 spawnPosition, bool isBoss)
    {
        // Instantiate the zombie
        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = zombie.GetComponent<Enemy>();
        enemyScript.isBoss = isBoss; // Set the boss property
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
