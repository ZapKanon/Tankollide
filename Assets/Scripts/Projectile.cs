using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    protected Rigidbody2D projectileRigidbody;
    public float shotSpeed;
    public float damage;
    public int team;
    public bool collided;

    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
        collided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            Move();
        }
    }

    /// <summary>
    /// Travel through space after being fired
    /// </summary>
    public void Move()
    {
        projectileRigidbody.velocity = (shotSpeed * direction.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If colliding with a character
        if (collision.gameObject.TryGetComponent(out Character targetCharacter))
        {
            //Only collide with characters on other teams
            if (targetCharacter.team != team)
            {
                targetCharacter.health -= damage;
                Destroy(gameObject);
            }
        }
        //If colliding with another projectile
        else if (collision.gameObject.TryGetComponent(out Projectile targetProjectile))
        {
            collided = true;
            projectileRigidbody.velocity = Vector2.zero;
        }
    }
}
