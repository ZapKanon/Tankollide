using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
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
    }
}
