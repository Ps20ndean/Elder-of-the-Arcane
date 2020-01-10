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
                    whereToSpawn = new Vector2(transform.position.x, transform.position.y);
                    var clone = Instantiate(enemy, whereToSpawn, Quaternion.identity);
                    cloneCount++;
                    clone.transform.parent = overallEnemies;
                }
            }
        }   
    }
}
