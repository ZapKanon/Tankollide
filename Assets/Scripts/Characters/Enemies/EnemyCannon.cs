using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cannon enemies remain stationary and rotate while shooting.
//They fire shots without aiming toward the player.
public class EnemyCannon : Enemy
{
    private IEnumerator rotateCoroutine;

    new void Update()
    {
        base.Update();
        Shoot();

        if (fireRateTimer == 0)
        {
            StartRotation(45, true);
        }
    }

    //Rotate this cannon so that its next shot will travel in a different direction
    public void StartRotation(float degrees, bool clockwise)
    {
        Vector3 goalRotation = transform.rotation.eulerAngles + new Vector3(0, 0, -degrees);

        if (clockwise)
        {
            //StopCoroutine(rotateCoroutine);
            StartCoroutine(rotateCoroutine = Rotate(goalRotation));
        }
        else
        {
            //transform.Rotate(Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 0, degrees), Time.deltaTime));
        }
    }

    IEnumerator Rotate(Vector3 goalRotation)
    {
        while (transform.rotation != Quaternion.Euler(goalRotation))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(goalRotation), Time.deltaTime * 100);
            yield return null;
        }
    }
}
