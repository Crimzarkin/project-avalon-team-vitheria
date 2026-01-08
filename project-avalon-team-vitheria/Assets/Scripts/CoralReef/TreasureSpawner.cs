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
        generateTreasure(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
