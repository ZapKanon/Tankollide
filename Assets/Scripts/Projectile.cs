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
    public CollisionProjectile collisionProjectile;
    protected bool collided;

    // Start is called before the first frame update
    public virtual void Start()
    {
        collided = false;
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Move();
    }

    /// <summary>
    /// Travel through space after being fired
    /// </summary>
    public void Move()
    {
        projectileRigidbody.velocity = (shotSpeed * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If colliding with a character
        if (collision.gameObject.TryGetComponent(out Character targetCharacter))
        {
            CollisionWithCharacter(targetCharacter);
        }
        //If colliding with another projectile
        else if (collision.gameObject.TryGetComponent(out StandardProjectile targetStandardProjectile))
        {
            CollisionWithStandardProjectile(targetStandardProjectile);
        }
        else if (collision.gameObject.TryGetComponent(out CollisionProjectile targetCollisionProjectile))
        {
            CollisionWithCollisionProjectile(targetCollisionProjectile);
        }
        else if (collision.gameObject.TryGetComponent(out Wall targetWall))
        {
            CollisionWithWall(targetWall);
        }
    }

    public virtual void CollisionWithCharacter(Character targetCharacter)
    {
        //Only collide with characters on other teams
        if (targetCharacter.team != team)
        {
            targetCharacter.TakeDamage(damage);

            //Destroy this projectile
            Destroy(gameObject);
        }
    }

    public virtual void CollisionWithStandardProjectile(StandardProjectile targetProjectile)
    {
        if (!collided)
        {
            //Create a collision projectile, merging the parameters of the two existing projectiles
            CollisionProjectile newCollision = Instantiate(collisionProjectile, Vector3.Lerp(transform.position, targetProjectile.transform.position, 0.5f), Quaternion.identity);
            newCollision.direction = direction + targetProjectile.direction;
            newCollision.shotSpeed = (shotSpeed + targetProjectile.shotSpeed) / 2;
            newCollision.damage = damage + targetProjectile.damage;

            targetProjectile.collided = true;
            collided = true;
        }

        //Destroy both projectiles
        Destroy(targetProjectile);
        Destroy(gameObject);
    }

    public virtual void CollisionWithCollisionProjectile(CollisionProjectile targetProjectile)
    {
        
    }

    public virtual void CollisionWithWall(Wall targetWall)
    {
        Destroy(gameObject);
    }
}
