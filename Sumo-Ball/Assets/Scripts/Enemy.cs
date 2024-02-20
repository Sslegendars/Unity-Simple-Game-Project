using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject _player;
    [SerializeField]
    private float _speed = 3.0f;

    public bool isBoss = false;
    public float spawnInterval;
    private float nextSpawn;
    public int miniEnemySpawnCount;
    private SpawnManager spawnManager;
        
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();        
        _player = GameObject.Find("Player");

        if(isBoss)
        {
            spawnManager = FindAnyObjectByType<SpawnManager>();
        }
    }   
    void Update()
    {
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * _speed);
        if(isBoss)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
