using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpawnerController : MonoBehaviour
{
    public int initialZombiePerWave = 1;
    public int currentZombiePerWave;

    public int bossZombieCount = 1; // Counter for the number of boss zombies to spawn

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

    private void Start()
    {
        currentZombiePerWave = initialZombiePerWave;
        StartNextWave();
    }

    // Start the next wave
    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;
        currentWaveUI.text = "Wave: " + currentWave.ToString();
        
        // Increase the number of zombies per wave by 2 after the first wave
        if (currentWave > 1) {
            currentZombiePerWave += 2;  
        }

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiePerWave; i++)
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
            bossZombieCount++;  
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        // Generate a random offset for the spawn position
        Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f));
        return transform.position + spawnOffset;
    }

    private void InstantiateZombie(GameObject zombiePrefab, Vector3 spawnPosition)
    {
        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = zombie.GetComponent<Enemy>();
        currentZombiesAlive.Add(enemyScript);
    }

    // Update is called once per frame
    private void Update()
    {
        // Remove dead zombies from the list
        List<Enemy> zombiesToRemove = new List<Enemy>();
        foreach (Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        // Remove the dead zombies from the list
        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        // Check if all zombies are dead
        if (currentZombiesAlive.Count == 0 && !isCoolDown)
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
            coolDownCounter = waveCooldown;
        }
    }

    // Start the cooldown timer
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