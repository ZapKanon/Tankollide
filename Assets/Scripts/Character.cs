using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float rotateSpeed;
    public float health;
    public float maxHealth;
    public float fireRate;
    public float shotSpeed;

    protected float fireRateTimer;
    public int team;

    public GameObject shotOriginPoint;
    protected Rigidbody2D tankRigidbody;
    protected Canvas canvas;
    public Image healthOrb;
    public Projectile projectile;

    // Start is called before the first frame update
    public void Start()
    {
        tankRigidbody = GetComponent<Rigidbody2D>();
        canvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    protected void Update()
    {
        UpdateHealth();
        fireRateTimer -= Time.deltaTime;
    }

    //Freeze canvas rotation to keep health orb filling from bottom to top
    private void LateUpdate()
    {
        canvas.gameObject.transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Fill or empty the character's orb to reflect their current health
    /// </summary>
    public void UpdateHealth()
    {
        Debug.Log("HP");
        healthOrb.fillAmount = health / maxHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Create a projectile and fire it in a direction
    /// </summary>
    public virtual void Shoot()
    {
        //Only fire if enough time has passed since last shot
        if (fireRateTimer <= 0)
        {
            Projectile newProjectile = Instantiate(projectile, shotOriginPoint.transform.position, Quaternion.identity);
            newProjectile.direction = transform.up;
            newProjectile.shotSpeed = shotSpeed;
            newProjectile.damage = 10;

            fireRateTimer = fireRate;
        }
    }

    /// <summary>
    /// Character dies when health reaches 0
    /// </summary>
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
