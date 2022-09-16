using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Lists")]

    [Header("Room 1 Enemies")]
    public List<Enemy> room1EnemiesBase;
    public List<Enemy> room1EnemiesSecond;
    public List<Enemy> room1EnemiesThird;
    public List<Wall> room1Doors;

    [Header("Room 2 Enemies")]
    public List<Enemy> room2EnemiesBase;
    public List<Enemy> room2EnemiesSecond;
    public List<Enemy> room2EnemiesThird;
    public List<Wall> room2Doors;

    [Header("Room 3 Enemies")]
    public List<Enemy> room3EnemiesBase;
    public List<Enemy> room3EnemiesSecond;
    public List<Enemy> room3EnemiesThird;
    public List<Wall> room3Doors;

    [Header("Room 4 Enemies")]
    public List<Enemy> room4EnemiesBase;
    public List<Enemy> room4EnemiesSecond;
    public List<Enemy> room4EnemiesThird;
    public List<Wall> room4Doors;

    [Header("Hallway Enemies")]
    public List<Enemy> roomAEnemies;
    public List<Wall> roomADoors;
    public List<Enemy> roomBEnemies;
    public List<Wall> roomBDoors;
    public List<Enemy> roomCEnemies;
    public List<Wall> roomCDoors;

    [Header("Moon Enemy Data")]
    public float moonHealth;
    public float moonFireRate;

    [Header("Cannon Enemy Data")]
    public float cannonHealth;
    public float cannonFireRate;

    [Header("Chaser Enemy Data")]
    public float chaserHealth;
    public float chaserMoveSpeed;

    [Header("Shared References")]
    public StandardProjectile enemyProjectile;
    public Player player;
    public float shotSpeed;

    private List<Enemy> masterEnemyList = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        //Construct the master list containing all enemies in the game
        masterEnemyList.AddRange(room1EnemiesBase);
        masterEnemyList.AddRange(room1EnemiesSecond);
        masterEnemyList.AddRange(room1EnemiesThird);

        masterEnemyList.AddRange(room2EnemiesBase);
        masterEnemyList.AddRange(room2EnemiesSecond);
        masterEnemyList.AddRange(room2EnemiesThird);

        masterEnemyList.AddRange(room3EnemiesBase);
        masterEnemyList.AddRange(room3EnemiesSecond);
        masterEnemyList.AddRange(room3EnemiesThird);

        masterEnemyList.AddRange(roomAEnemies);
        masterEnemyList.AddRange(roomBEnemies);
        masterEnemyList.AddRange(roomCEnemies);

        SetEnemyParams();
    }

    /// <summary>
    /// Set up all enemies with necessary information
    /// </summary>
    public void SetEnemyParams()
    {
        foreach(Enemy enemy in masterEnemyList)
        {
            if (enemy is EnemyMoon)
            {
                enemy.maxHealth = moonHealth;
                enemy.health = moonHealth;
                enemy.fireRate = moonFireRate;
            }
            else if (enemy is EnemyCannon)
            {
                enemy.maxHealth = cannonHealth;
                enemy.health = cannonHealth;
                enemy.fireRate = cannonFireRate;
            }
            else if (enemy is EnemyChaser)
            {
                enemy.maxHealth = chaserHealth;
                enemy.health = chaserHealth;
                enemy.moveSpeed = chaserMoveSpeed;
            }

            //Params applied to all enemies
            enemy.shotSpeed = shotSpeed;
            enemy.targetCharacter = player;
            enemy.projectile = enemyProjectile;
        }
    }
}
