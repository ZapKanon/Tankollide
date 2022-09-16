using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public LineRenderer aimingLine;
    public GameObject crosshair;
    private Camera mainCamera;
    public Image crosshairFill;
    public Image crosshairLockOn;
    private RaycastHit2D cursorHit;
    private Vector3 crosshairRegularScale;
    private Vector3 crosshairLargeScale;
    private Vector3 cursorPosition;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        team = 0;
        mainCamera = Camera.main;
        crosshairRegularScale = crosshair.transform.localScale;
        crosshairLargeScale = new Vector3(2.2f, 2.2f, 1);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        PlayerInput();
        FaceCursor();
        DrawAimingLine();
    }

    private void FixedUpdate()
    {  
        MoveCamera();
        CursorRaycast();
    }

    private void OnGUI()
    {
        crosshair.transform.position = cursorPosition;
    }

    /// <summary>
    /// The player can move in 8 directions using WASD input
    /// The player's model rotates in the direction of movement
    /// </summary>
    private void PlayerInput()
    {
        Vector3 positionChange = new Vector3(0.0f, 0.0f, 0.0f);
        float moveSpeed = base.moveSpeed;

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

        //TODO: Add gamepad input

        tankRigidbody.velocity = (moveSpeed * positionChange.normalized);

        //Rotate jets to face the opposite direction of movement
        //Hide jets if there is no movement
        if (positionChange == Vector3.zero)
        {
            jetsPivot.transform.localScale = new Vector3(1, Mathf.Clamp(jetsPivot.transform.localScale.y - Time.deltaTime * 5, 0, 1), 1);          
        }
        else
        {
            jetsPivot.transform.localScale = new Vector3(1, Mathf.Clamp(jetsPivot.transform.localScale.y + Time.deltaTime * 20, 0, 1), 1);

            float dot = Vector3.up.x * positionChange.x + Vector3.up.y * positionChange.y;
            float det = Vector3.up.x * positionChange.y - Vector3.up.y * positionChange.x;
            float angle = Mathf.Rad2Deg * Mathf.Atan2(det, dot);
            jetsPivot.transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(jetsPivot.transform.rotation.eulerAngles.z, angle, Time.deltaTime * 8));
        }

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
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        cursorPosition.z = 0;

        Vector2 playerRotation = new Vector2(
            cursorPosition.x - transform.position.x, 
            cursorPosition.y - transform.position.y);

        float playerDirection = Mathf.Atan2(playerRotation.y, playerRotation.x);
        playerDirection = Mathf.Rad2Deg * playerDirection;

        tankRigidbody.rotation = playerDirection - 90;
    }

    /// <summary>
    /// Draw a line from the player's tank to their cursor position
    /// </summary>
    public void DrawAimingLine()
    {
        Vector3[] points = new Vector3[2];

        //Start at player's position
        points[0] = transform.position;

        //End at cursor position
        points[1] = cursorPosition;

        aimingLine.SetPositions(points);
    }

    /// <summary>
    /// Set camera position between player's position and cursor position
    /// </summary>
    public void MoveCamera()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 midPoint = (transform.position + cursorPosition) / 2;
        Vector3 newCameraPosition = (Vector2)midPoint - (Vector2)transform.position;
        float maxDistanceX = 7;
        float maxDistanceY = 3.5f;

        //Clamp cursor position values to keep player onscreen at all times
        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, -maxDistanceX, maxDistanceX);
        newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, -maxDistanceY, maxDistanceY);
        midPoint = transform.position + newCameraPosition;

        //Set camera position

        //TODO: Reduce camera jitter with Lerping / MoveTowards
        newCameraPosition = midPoint;
        mainCamera.transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, -10);
        //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(newCameraPosition.x, newCameraPosition.y, -10), Time.deltaTime * 15);
    }

    public override void UpdateRecharge()
    {
        base.UpdateRecharge();
        crosshairFill.fillAmount = 1 - fireRateTimer / fireRate;
    }

    //Check if the cursor is colliding with any relevant objects
    public void CursorRaycast()
    {
        //cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        cursorHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (cursorHit.collider != null)
        {
            //Debug.Log(cursorHit.collider.name);
            //Enemy or projectile collider
            if (cursorHit.collider.gameObject.TryGetComponent(out Enemy hoveredEnemy) || (cursorHit.collider.gameObject.TryGetComponent(out Projectile hoveredProjectile) && hoveredProjectile.team != 0))
            {
                //Enlarge the crosshair when hovering over an enemy or projectile
                crosshair.transform.localScale = Vector3.Lerp(crosshair.transform.localScale, crosshairLargeScale, Time.deltaTime * 10);
                crosshairLockOn.enabled = true;
            }
            else
            {
                //Return the crosshair to normal size
                crosshair.transform.localScale = Vector3.Lerp(crosshair.transform.localScale, crosshairRegularScale, Time.deltaTime * 10);
                crosshairLockOn.enabled = false;
            }
        }
        else
        {
            //Return the crosshair to normal size
            crosshair.transform.localScale = Vector3.Lerp(crosshair.transform.localScale, crosshairRegularScale, Time.deltaTime * 10);
            crosshairLockOn.enabled = false;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        mainCamera.GetComponent<CameraShake>().TriggerShake(damage);
    }
}
