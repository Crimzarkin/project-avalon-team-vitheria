using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreasureSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] treasurePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        populateSpawnPoints();
        generateTreasure(2);
    }

    // Adds treasure items to unique random spots in the map.
    void generateTreasure(int amount = 5)
    {
        int[] randPositions = {};
        int numOfPositions = spawnPoints.Length;
        int position;
        
        if (amount > numOfPositions)
        {
            throw new ArgumentOutOfRangeException(nameof(amount),"Amount of objects exceeds available spawn locations");
        }

        for (int loop = 0; loop < amount; loop++)
        {
            position = Random.Range(0,numOfPositions);
            while (randPositions.Contains(position))
            {
                position = Random.Range(0,amount);
            }
            randPositions.Append(position);
            Instantiate(treasurePrefabs[0], spawnPoints[position].position, transform.rotation);
        }
    }

    // Fetches all tagged spawnPoints in the scene 
    void populateSpawnPoints()
    {
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("ItemSpawnPoint");
        spawnPoints = new Transform[spawnPointObjects.Length];
        for (int spawnPoint = 0; spawnPoint < spawnPointObjects.Length; spawnPoint++)
        {
            spawnPoints[spawnPoint] = spawnPointObjects[spawnPoint].transform;
        }
    }
}
