using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public string ID;
    private SpriteRenderer icon;
    private Vector3 defaultScale;
    private Vector3 bigScaleIncrease;
    private float pulseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponentInChildren<SpriteRenderer>();
        defaultScale = icon.gameObject.transform.localScale;
        bigScaleIncrease = new Vector3(0.2f, 0.2f, 1);
        pulseSpeed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        //Pulse size of icon
        Vector3 newScale = new Vector3(defaultScale.x + Mathf.PingPong(Time.time * pulseSpeed, bigScaleIncrease.x), defaultScale.y + Mathf.PingPong(Time.time * pulseSpeed, bigScaleIncrease.y), 1);

        icon.gameObject.transform.localScale = newScale;
    }
}
