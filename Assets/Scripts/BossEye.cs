using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEye : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D tankRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        tankRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget(target);
    }

    public void FaceTarget(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.z = 0;

        Vector2 enemyRotation = new Vector2(
            targetPosition.x - transform.position.x,
            targetPosition.y - transform.position.y);

        float enemyDirection = Mathf.Atan2(enemyRotation.y, enemyRotation.x);
        enemyDirection = Mathf.Rad2Deg * enemyDirection;

        tankRigidbody.rotation = enemyDirection - 270;
    }
}
