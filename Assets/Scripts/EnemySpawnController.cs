using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPref;
    [Range(0.0f, 10.0f)][SerializeField] float SpawnRate = 0.5f;
    [SerializeField] int difficulty = 1;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    void Update()
    {

    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (GameManager.Instance.difficulty != difficulty)
            {
                difficulty = GameManager.Instance.difficulty;
                if (difficulty <= 20)
                {
                    SpawnRate += 0.5f;
                }
            }
            yield return new WaitForSeconds(1 / SpawnRate);
            float randomEnemy = Random.Range(0.0f, 1.0f);
            if (randomEnemy < GameManager.Instance.difficulty * 0.1f)
                Instantiate(enemyPref[1], transform.position, Quaternion.identity);
            else
                Instantiate(enemyPref[0], transform.position, Quaternion.identity);

        }
    }
}
