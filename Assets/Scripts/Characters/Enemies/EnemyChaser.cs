using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Chaser enemies constantly move towards the player.
//They do not fire shots, but heavily damage the player on contact by exploding.
public class EnemyChaser : Enemy
{
    new void Update()
    {
        base.Update();
        Follow(targetCharacter.gameObject);
    }

    public void Follow(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        direction = direction.normalized;
        tankRigidbody.velocity = moveSpeed * direction;

        Vector2 dir = tankRigidbody.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
