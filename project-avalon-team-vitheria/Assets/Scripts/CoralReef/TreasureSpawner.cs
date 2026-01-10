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
        int[] randPositions = new int[amount];
        int numOfPositions = spawnPoints.Length;
        int treasurePosition;
        int nullVal = -1;
        
        // Sets array values to -1 to allow 0 to be a unique value for possible positions
        for (int pos = 0; pos < amount; pos++)
        {
            randPositions[pos] = nullVal;
        }

        if (amount > numOfPositions)
        {
            throw new ArgumentOutOfRangeException(nameof(amount),"Amount of treasure exceeds available spawn locations");
        }

        for (int loop = 0; loop < amount; loop++)
        {
            do
            {
                treasurePosition = Random.Range(0,numOfPositions);
            } while (randPositions.Contains(treasurePosition));
            randPositions[loop] = treasurePosition;
            Instantiate(treasurePrefabs[0], spawnPoints[treasurePosition].position, transform.rotation);
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
