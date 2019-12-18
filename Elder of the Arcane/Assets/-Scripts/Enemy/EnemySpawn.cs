using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float TheNumber;
    public GameObject enemy;
    float randX;
    Vector2 whereToSpawn;
    public Transform overallEnemies;
    public float spawnRate = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time > TheNumber)
        {
            TheNumber = Time.time + spawnRate;
            randX = Random.Range(-8.4f, 8.4f);
            whereToSpawn = new Vector2(randX, transform.position.y);
            var clone = Instantiate(enemy, whereToSpawn, Quaternion.identity);
            clone.transform.parent = overallEnemies;
        }   
    }
}
