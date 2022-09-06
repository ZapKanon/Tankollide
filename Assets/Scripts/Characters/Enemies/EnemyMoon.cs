using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moon enemies remain stationary while constantly facing the player.
//They fire shots at the player at regular intervals.
public class EnemyMoon : Enemy
{
    new void Update()
    {
        base.Update();
        FaceTarget(targetCharacter.gameObject);
        Shoot();
    }

    /// <summary>
    /// Always face in the direction of the target character
    /// </summary>
    public void FaceTarget(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.z = 0;

        Vector2 enemyRotation = new Vector2(
            targetPosition.x - transform.position.x,
            targetPosition.y - transform.position.y);

        float enemyDirection = Mathf.Atan2(enemyRotation.y, enemyRotation.x);
        enemyDirection = Mathf.Rad2Deg * enemyDirection;

        tankRigidbody.rotation = enemyDirection - 90;
    }
}
