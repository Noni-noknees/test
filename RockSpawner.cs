using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject RockPrefab;
    public int numberOfRocks = 100;
    public float spawnAreaSize = 50.0f;
    public float rockspawnsize = 50.0f;
    public int ylocation = 0;
    public int increase = 0;
    public int y = 0;
    public int editz = 0;
    public int editx = 0;
    void Start()
    {
        SpawnTreasures();
    }
    void SpawnTreasures()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            Vector3 randomPosition = new Vector3(Mathf.Sin(30 * i) * Random.Range(-spawnAreaSize + ylocation, spawnAreaSize + ylocation)-editx, y + Random.Range(-rockspawnsize, rockspawnsize), Mathf.Cos(30 * i) * Random.Range(-spawnAreaSize + ylocation, spawnAreaSize + ylocation)-editz);
            Instantiate(RockPrefab, randomPosition, Quaternion.identity);
        }
    }
}


