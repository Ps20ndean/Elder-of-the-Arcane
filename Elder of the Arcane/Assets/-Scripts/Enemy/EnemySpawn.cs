using UnityEngine;

public class EnemySpawn : EnemyAI
{
    public float maxClones;
    public GameObject enemy;
    Vector2 whereToSpawn;
    public Transform overallEnemies;
    public float cloneCount = 0f;

    public new void Start()
    {
        //On start, begin tracking player position
        enemyParameterCheck();
    }

    public new void Update()
    {
        //Every tick, use tracked player position and position of object to determine if in range
        Distance();
        //If player is alive
        if (target)
        {
            //If within distance
            if (inDist)
            {
                //if max number of clones is greater than the clone count, spawn a clone and increase clone count
                if (maxClones > cloneCount)
                {
                    // decides where to spawn the clone within a certain range
                    whereToSpawn = new Vector2(transform.position.x, transform.position.y);

                    // sets the variable of clone to what to spawn
                    var clone = Instantiate(enemy, whereToSpawn, Quaternion.identity);

                    // adds one to the clone counter as to not overload the amount of clones on screen
                    cloneCount++;

                    // sets the parent to the enemy spawner
                    clone.transform.parent = overallEnemies;
                }
            }
        }   
    }
}
