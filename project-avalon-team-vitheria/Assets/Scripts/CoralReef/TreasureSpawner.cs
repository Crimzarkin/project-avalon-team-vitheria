using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TreasureSpawner : MonoBehaviour
{
    private Transform[] spawnPoints;
    [SerializeField] private GameObject[] treasurePrefabs;
    [SerializeField] private int treasureAmount;


    // Start is called before the first frame update
    void Start()
    {
        populateSpawnPoints();
        generateTreasure(treasureAmount);
    }

    // Adds treasure items to unique random spots in the map.
    private void generateTreasure(int amount = 5)
    {
        int[] randPositions = new int[amount];
        int numOfPositions = spawnPoints.Length;
        int treasurePosition;
        int treasurePrefabSelector;
        int nullVal = -1;
        // Sets array values to -1 to allow 0 to be a unique value for possible positions
        for (int pos = 0; pos < amount; pos++)
        {
            randPositions[pos] = nullVal;
        }

        if (amount > numOfPositions)
        {
            throw new ArgumentOutOfRangeException(nameof(amount),"Amount of treasure exceeds available spawn locations: " + numOfPositions);
        }
        GameObject folder = new GameObject("Coins");
        for (int loop = 0; loop < amount; loop++)
        {
            do
            {
                treasurePosition = Random.Range(0,spawnPoints.Length);
                treasurePrefabSelector = Random.Range(0,treasurePrefabs.Length);
            } while (randPositions.Contains(treasurePosition));
            randPositions[loop] = treasurePosition;
            Instantiate(treasurePrefabs[treasurePrefabSelector], spawnPoints[treasurePosition].position, transform.rotation, folder.transform);
        }
        GameObject.Find("Player").GetComponent<TreasureCounterHUD>().setcounterText(amount);
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
