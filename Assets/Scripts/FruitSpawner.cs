using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct FruitMapping
{
    public FruitType type;
    public GameObject prefab; 
}

public class FruitSpawner : MonoBehaviour
{
    public FruitMapping[] mappings;
    private Dictionary<FruitType, GameObject> _map;
    public float minSpacing = 1.5f;
    void Awake()
    {
        _map = new Dictionary<FruitType, GameObject>();
        foreach (var m in mappings)
        {
            if (m.prefab != null && !_map.ContainsKey(m.type))
                _map.Add(m.type, m.prefab);
        }
    }

    public GameObject SpawnFruit(FruitType type, Vector2 position)
    {
        var prefab = _map[type];
        var go = Instantiate(prefab, (Vector3)position, Quaternion.identity, transform);
        return go;
    }

    public void SpawnRandomFruits(int count, Vector2 min, Vector2 max)
    {
        var types = (FruitType[])System.Enum.GetValues(typeof(FruitType));
        var spawnedPositions = new List<Vector2>();

        for (int i = 0; i < count; i++)
        {
            Vector2 pos;
            int attempts = 0;

            do
            {
                pos = new Vector2(
                    Random.Range(min.x, max.x),
                    Random.Range(min.y, max.y)
                );
                attempts++;
                if (attempts > 50) break; 
            }
            while (spawnedPositions.Exists(p => Vector2.Distance(p, pos) < minSpacing));

            spawnedPositions.Add(pos);

            var t = types[Random.Range(0, types.Length)];
            SpawnFruit(t, pos);
        }
    }
}