using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRing : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        team = 1;
    }

    // Update is called once per frame
    new void Update()
    {
        //Skip normal damage-taking behavior
        if (health < 1)
        {
            Die();
        }
    }

    new void LateUpdate()
    {

    }
}
