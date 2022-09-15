using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkPointPrefab;
    [SerializeField] GameObject[] PowerUpPrefab;
    [SerializeField] int checkPointSpawnDelay = 10;
    [SerializeField] int checkPointSpawnArea = 10;
    [SerializeField] int powerUpSpawnDelay = 10;
    [SerializeField] int powerUpSpawnArea = 10;
    void Start()
    {
        StartCoroutine(SpawnCheckPoints());
        StartCoroutine(SpawnPowerUps());
    }
    IEnumerator SpawnCheckPoints()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkPointSpawnDelay);
            Vector2 randomPos = Random.insideUnitCircle * checkPointSpawnArea;
            Instantiate(checkPointPrefab, new Vector3(randomPos.x, randomPos.y, -2), Quaternion.identity);
        }
    }
    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnDelay);
            Vector2 randomPos = Random.insideUnitCircle * powerUpSpawnArea;
            int randomPower = Random.Range(0, PowerUpPrefab.Length);
            Instantiate(PowerUpPrefab[randomPower], new Vector3(randomPos.x, randomPos.y, -2), Quaternion.identity);
        }
    }
}
