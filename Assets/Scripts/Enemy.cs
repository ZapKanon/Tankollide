using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Character targetCharacter;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        team = 1;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        Shoot();
        FaceTarget(targetCharacter.gameObject);
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
