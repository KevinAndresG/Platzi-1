using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform player;
    public int health;
    int damage = 1;
    [SerializeField] int difficulty = 1;
    [SerializeField] int ScorePerEnemy = 50;
    [SerializeField] float enemySpeed;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("EnemiesSpawn");
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[randomSpawnPoint].transform.position;
    }
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        // transform.position += (Vector3)direction/direction.magnitude * Time.deltaTime * enemySpeed;
        transform.position += (Vector3)direction.normalized * Time.deltaTime * enemySpeed;
        if (difficulty != GameManager.Instance.difficulty)
        {
            enemySpeed += 0.2f;
            difficulty = GameManager.Instance.difficulty;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.Score += ScorePerEnemy;
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
            other.GetComponent<PlayerController>().Invulnerability = true;
        }
    }

}
