using UnityEngine;

public class AIspawner : MonoBehaviour
{
    public GameObject AIbot;
    public int numberOfAI = 10;
    public float spawnAreaSizes = 50.0f;
    public int edity = 0;
    public int z = 0;
    public int x = 0;

    void Start()
    {
        SpawnAI();
    }

    void SpawnAI()
    {
        for (int i = 0; i < numberOfAI; i++)
        {
            Vector3 randomPosition = new Vector3((Random.Range(-spawnAreaSizes, spawnAreaSizes) + x), edity, (Random.Range(-spawnAreaSizes, spawnAreaSizes) + z));
            Instantiate(AIbot, randomPosition, Quaternion.identity);
        }
    }
}
