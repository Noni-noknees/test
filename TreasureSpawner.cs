using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    public GameObject treasurePrefab;
    public int numberOfTreasures = 10;
    public float spawnAreaSize = 50.0f;
    public int y = 0;
    public int editz = 0;
    public int editx = 0;

    void Start()
    {
        SpawnTreasures();
    }

    void SpawnTreasures()
    {
        for (int i = 0; i < numberOfTreasures; i++)
        {
            Vector3 randomPosition = new Vector3((Random.Range(-spawnAreaSize, spawnAreaSize)+editx), y, (Random.Range(-spawnAreaSize, spawnAreaSize)+editz));
            Instantiate(treasurePrefab, randomPosition, Quaternion.identity);
        }
    }
}
