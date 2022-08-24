using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProjectile : Projectile
{
    public int shotsAbsorbed;
    public float collisionDamageMultiplier;
    public float collisionSpeedIncrement;
    private Vector3 initialScale;

    public override void Start()
    {
        base.Start();
        damage *= collisionDamageMultiplier;
        initialScale = transform.localScale;
    }

    public override void CollisionWithStandardProjectile(StandardProjectile targetProjectile)
    {
        //Increase the size of this projectile
        shotsAbsorbed++;
        transform.localScale =  initialScale * (shotsAbsorbed / 2.0f);
        damage = (damage + targetProjectile.damage) * collisionDamageMultiplier;
        direction = targetProjectile.direction;
        shotSpeed += collisionSpeedIncrement;

        //Absorb (destroy) the standard projectile
        Destroy(targetProjectile.gameObject);
    }

    public override void CollisionWithCollisionProjectile(CollisionProjectile targetProjectile)
    {

    }
}
