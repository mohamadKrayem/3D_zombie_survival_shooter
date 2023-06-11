using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject enemy;

    // The number of enemies spawned in the game
    public int spawnCount = 0;

    // The bounds of the map
    public int minXPos = -190;
    public int maxXPos = 190;
    public int minZPos = 100;
    public int maxZPos = 400;

    // The maximum number of enemies allowed in the game, 
    // this number can be changed in the inspector.
    public int maxEnemies = 20;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // this method handles the spawning of enemies in the game within the bounds of the map,
    // and within the maximum number of enemies allowed in the game.
    IEnumerator SpawnEnemies()
    {
      // while the number of enemies spawned is less than the maximum number of enemies allowed in the game
      // then spawn an enemy.
         while (spawnCount < maxEnemies)
        {
         // Get a random position within the bounds of the map
            Vector3 spawnPosition = GetRandomSpawnPosition();
            // Instantiate the enemy at the spawn position
            GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(0.1f);
            spawnCount++;
        }
    }

   // This method returns a random position within the bounds of the map
    Vector3 GetRandomSpawnPosition()
    {
        float xPos = Random.Range(minXPos, maxXPos);
        float zPos = Random.Range(minZPos, maxZPos);
        return new Vector3(xPos, 3f, zPos);
    }
}
