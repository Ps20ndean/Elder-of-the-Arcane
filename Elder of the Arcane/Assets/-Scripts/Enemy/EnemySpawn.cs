using UnityEngine;

public class EnemySpawn : EnemyAI
{
    public GameObject enemy;
    Vector2 whereToSpawn;
    public Transform overallEnemies;

    public new void Start()
    {
        //On start, begin tracking player position
        enemyParameterCheck();
        InvokeRepeating("spawnSpicyBois", 2.5f, 3f);
    }

    public void spawnSpicyBois()
    {
        if (inDist)
        {
            whereToSpawn = new Vector2(transform.position.x, transform.position.y);

            Instantiate(enemy, whereToSpawn, transform.rotation);
        }
        else
        {
            // do nothing
        }
    }

    public new void Update()
    {
        //Every tick, use tracked player position and position of object to determine if in range
        Distance();
        }
    }

