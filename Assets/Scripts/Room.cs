using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains a collider representing the room bounds, manages enemies and doors
public class Room : MonoBehaviour
{
    public List<Door> doors;
    public List<Enemy> enemiesBase; //Enemies that are always present in the room
    public List<Enemy> enemiesSecond; //Enemies that appear if this is not the player's first room
    public List<Enemy> enemiesThird; //Enemies that appear if this is not the player's second room

    public List<Enemy> allEnemies;

    public GameObject reward; //Something given to the player upon completing the room

    public bool active;
    public bool completed;
    //TODO public Powerup powerup;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        active = false;
        //reward.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        allEnemies.AddRange(enemiesBase);
        allEnemies.AddRange(enemiesSecond);
        allEnemies.AddRange(enemiesThird);

        foreach (Door door in doors)
        {
            door.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is in this room, check if the room has been cleared
        if (active)
        {
            if (IsCleared() && !completed)
            {
                Completed();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player enters the room and the room hasn't already been completed
        if (collision.gameObject.TryGetComponent(out Player player) && !completed)
        {
            //Spawn appropriate enemies based on number of prior rooms completed
            foreach (Enemy enemy in enemiesBase)
            {
                enemy.gameObject.SetActive(true);
            }

            if (gameManager.roomsCompleted <= 1)
            {
                foreach (Enemy enemy in enemiesSecond)
                {
                    enemy.gameObject.SetActive(true);
                }
            }

            if (gameManager.roomsCompleted <= 2)
            {
                foreach (Enemy enemy in enemiesThird)
                {
                    enemy.gameObject.SetActive(true);
                }
            }

            //Spawn doors to prevent player from leaving the room
            foreach (Door door in doors)
            {
                door.gameObject.SetActive(true);
            }

            active = true;
        }
    }

    /// <summary>
    /// Check if all enemies have been defeated
    /// </summary>
    public bool IsCleared()
    {
        //Return false if any enemies are alive
        foreach (Enemy enemy in allEnemies)
        {
            if (enemy.isActiveAndEnabled)
            {
                return false;
            }
        }

        return true;
    }

    //Spawn a powerup reward and remove all doors when the room is completed
    public void Completed()
    {
        completed = true;

        foreach (Door door in doors)
        {
            door.gameObject.SetActive(false);
        }

        //Spawn a reward for completing the room
        if (reward != null)
        {
            reward.transform.position = transform.position;
            reward.SetActive(true);
        }
    }
}
