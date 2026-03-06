using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemySpawnDuration = 1;
    private float enemySpawnTimer;

    void Update()
    {
        enemySpawnTimer -= Time.deltaTime;
        if (enemySpawnTimer <= 0)
        {
            enemySpawnTimer = enemySpawnDuration;

            GameObject enemyObject = Instantiate(enemyPrefab);
            enemyObject.transform.SetParent(this.transform);
            enemyObject.transform.position = new Vector3(
                Random.Range(-5f, 5f),
                7,
                0
            );
        }
    }
}