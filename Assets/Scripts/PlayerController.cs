using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform aim;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] Camera cam;
    // [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 bounceVel;
    Vector2 facingDirection;
    [SerializeField] int playerSpeed;
    [SerializeField] float aimOffset;
    float H;
    float V;
    // [SerializeField] int reloadTime;
    [SerializeField] int health;
    [SerializeField] float fireRate;
    bool canShoot;
    public bool Invulnerability = false;
    public int InvulnerableTime = 3;
    // [SerializeField] int ammo;
    // [SerializeField] int newAmmo;
    bool powerShootEnabled;
    void Start()
    {
        fireRate = 3f;
        // reloadTime = 2;
        // ammo = 10;
        // newAmmo = ammo;
        aimOffset = 2f;
        playerSpeed = 3;
        canShoot = true;
        health = 5;
        // rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        // Aim Movement
        facingDirection = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized * aimOffset;
        // Player Shoots
        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            // Bullet Direction
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);
            if (powerShootEnabled)
            {
                bulletClone.GetComponent<BulletController>().powerShoot = true;
                StartCoroutine(DisablePowerShoot());
            }
            else
            {
                bulletClone.GetComponent<BulletController>().powerShoot = false;
            }
            StartCoroutine(TimeBetweenShoots());
            // Reload The Gun
            // if (ammo == 0)
            // {
            //     StartCoroutine(ReloadGun());
            // }
        }
    }
    void FixedUpdate()
    {
        // Player Movement
        // rb.velocity = new Vector2(H * Time.deltaTime * playerSpeed, rb.velocity.y);
        // rb.velocity = new Vector2(rb.velocity.x, V * Time.deltaTime * playerSpeed);
        transform.position += new Vector3(H * Time.deltaTime * playerSpeed, V * Time.deltaTime * playerSpeed, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            switch (other.GetComponent<PowerUpController>().powerUp)
            {
                case PowerUpController.typeOfPowerUp.IncreaseFireRate:
                    fireRate++;
                    break;
                case PowerUpController.typeOfPowerUp.PowerShot:
                    powerShootEnabled = true;
                    break;
            }
            Destroy(other.gameObject, 0.1f);
        }
    }
    public void TakeDamage(int damage)
    {
        if (!Invulnerability)
        {
            health -= damage;
        }
        if (health == 0)
        {
            // Game Over
        }
        StartCoroutine(Vulnerable());
    }
    // IEnumerator ReloadGun()
    // {
    //     yield return new WaitForSeconds(reloadTime);
    //     ammo = newAmmo;
    //     canShoot = true;
    // }
    IEnumerator TimeBetweenShoots()
    {
        yield return new WaitForSeconds(1 / fireRate);
        canShoot = true;
    }
    IEnumerator Vulnerable()
    {
        yield return new WaitForSeconds(InvulnerableTime);
        Invulnerability = false;
    }
    IEnumerator DisablePowerShoot()
    {
        yield return new WaitForSeconds(15);
        powerShootEnabled = false;
    }
}
