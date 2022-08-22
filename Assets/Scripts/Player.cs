using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private LineRenderer aimingLine;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        aimingLine = GetComponentInChildren<LineRenderer>();
        team = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        PlayerInput();
        FaceCursor();
    }

    /// <summary>
    /// The player can move in 8 directions using WASD input
    /// The player's model rotates in the direction of movement
    /// </summary>
    private void PlayerInput()
    {
        Vector3 positionChange = new Vector3(0.0f, 0.0f, 0.0f);
        float moveSpeed = walkSpeed;

        //Move forward
        if (Input.GetKey(KeyCode.W))
        {
            positionChange += Vector3.up;
        }

        //Move left
        if (Input.GetKey(KeyCode.A))
        {
            positionChange += Vector3.left;
        }

        //Move backward
        if (Input.GetKey(KeyCode.S))
        {
            positionChange += Vector3.down;
        }

        //Move right
        if (Input.GetKey(KeyCode.D))
        {
            positionChange += Vector3.right;
        }

        tankRigidbody.velocity = (moveSpeed * positionChange.normalized);

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Always face toward the mouse cursor's position
    /// </summary>
    public void FaceCursor()
    {
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        cursorPosition.z = 0;

        Vector2 playerRotation = new Vector2(
            cursorPosition.x - transform.position.x, 
            cursorPosition.y - transform.position.y);

        float playerDirection = Mathf.Atan2(playerRotation.y, playerRotation.x);
        playerDirection = Mathf.Rad2Deg * playerDirection;

        tankRigidbody.rotation = playerDirection - 90;

        DrawAimingLine(cursorPosition);
    }

    /// <summary>
    /// Draw a line from the player's tank to their cursor position
    /// </summary>
    public void DrawAimingLine(Vector3 cursorPosition)
    {
        Vector3[] points = new Vector3[2];

        //Start at player's position
        points[0] = transform.position;

        //End at cursor position
        points[1] = cursorPosition;

        aimingLine.SetPositions(points);

        //TODO: Extend line past edge of screen
    }
}
