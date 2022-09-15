using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int bulletHealth = 3;
    [SerializeField] int bulletDamage = 1;
    public bool powerShoot;
    void Start()
    {
        speed = 15;
        Destroy(gameObject, 3);
    }
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(bulletDamage);
            if (!powerShoot)
            {
                Destroy(gameObject);
            }
            bulletHealth--;
            if (bulletHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
