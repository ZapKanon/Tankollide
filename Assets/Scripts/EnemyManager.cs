using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Rooms")]
    public List<Room> rooms;

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

    public List<Enemy> masterEnemyList;

    // Start is called before the first frame update
    void Start()
    {
        //Create the master list of all enemies in the game
        foreach (Room room in rooms)
        {
            masterEnemyList.AddRange(room.allEnemies);
        }

        SetEnemyParams();
    }

    private void Update()
    {

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

            enemy.gameObject.SetActive(false);
        }
    }
}
