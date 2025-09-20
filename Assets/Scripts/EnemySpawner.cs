using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Vector2 minSpawn = new Vector2(-65, 0);
    public Vector2 maxSpawn = new Vector2(65, 0);
    public Transform spawnParent;

    public List<Enemy> SpawnEnemies(int count)
    {
        var spawned = new List<Enemy>();
        if (enemyPrefab == null) return spawned;

        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(minSpawn.x, maxSpawn.x),
                Random.Range(minSpawn.y, maxSpawn.y)
            );

            var go = Instantiate(enemyPrefab.gameObject, (Vector3)pos, Quaternion.identity, spawnParent);
            var enemy = go.GetComponent<Enemy>();
            if (enemy != null) spawned.Add(enemy);
        }

        return spawned;
    }

    public List<Enemy> SpawnEnemies(int count, float patrolHalfWidth, float safeDistanceFromPlayer = 5f)
    {
        var spawned = new List<Enemy>();
        if (enemyPrefab == null) return spawned;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerX = player != null ? player.transform.position.x : 0f;

        float leftMax = Mathf.Min(playerX - safeDistanceFromPlayer - patrolHalfWidth, maxSpawn.x - patrolHalfWidth);
        float rightMin = Mathf.Max(playerX + safeDistanceFromPlayer + patrolHalfWidth, minSpawn.x + patrolHalfWidth);

        for (int i = 0; i < count; i++)
        {
            float x;
            if (Random.value < 0.5f && leftMax > minSpawn.x + patrolHalfWidth)
            {
                x = Random.Range(minSpawn.x + patrolHalfWidth, leftMax);
            }
            else if (rightMin < maxSpawn.x - patrolHalfWidth)
            {
                x = Random.Range(rightMin, maxSpawn.x - patrolHalfWidth);
            }
            else
            {
                x = Random.Range(minSpawn.x + patrolHalfWidth, maxSpawn.x - patrolHalfWidth);
            }

            float y = Random.Range(minSpawn.y, maxSpawn.y);
            Vector2 pos = new Vector2(x, y);

            var go = Instantiate(enemyPrefab.gameObject, (Vector3)pos, Quaternion.identity, spawnParent);
            var enemy = go.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.InitializePatrol(new Vector2(pos.x - patrolHalfWidth, pos.y), new Vector2(pos.x + patrolHalfWidth, pos.y));
                spawned.Add(enemy);
            }
        }

        return spawned;
    }
}
