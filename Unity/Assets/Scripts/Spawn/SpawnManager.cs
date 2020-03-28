using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private SpawnTypeToPrefabMapping spawnTypeToPrefabMapping;

    private List<SpawnPoint> _spawnPoints;

    private void Awake()
    {
        _spawnPoints = FindObjectsOfType<SpawnPoint>().ToList();
    }

    public void Spawn<T>(SpawnType spawnType, Action<T> onSpawned = null) where T : MonoBehaviour
    {
        GameObject spawnPrefab = spawnTypeToPrefabMapping.GetSpawnPrefabForSpawnType(spawnType);

        T spawnedObject = Spawn<T>(spawnPrefab, GetSpawnPointFromSpawnType(spawnType));
        
        onSpawned?.Invoke(spawnedObject);
    }

    public void SpawnAllWithType(SpawnType spawnType)
    {
        GameObject spawnPrefab = spawnTypeToPrefabMapping.GetSpawnPrefabForSpawnType(spawnType);
        foreach (var spawnPoint in _spawnPoints)
        {
            if (spawnPoint.SpawnType == spawnType)
            {
                Spawn<MonoBehaviour>(spawnPrefab, spawnPoint);
            }
        }
    }

    public GameObject Spawn(SpawnPoint spawnPoint)
    {
        GameObject spawnPrefab = spawnTypeToPrefabMapping.GetSpawnPrefabForSpawnType(spawnPoint.SpawnType);
        return Instantiate(spawnPrefab, spawnPoint.Position, spawnPoint.Rotation);
    }

    private T Spawn<T>(GameObject spawnPrefab, SpawnPoint spawnPoint)
    {
        return Instantiate(spawnPrefab, spawnPoint.Position, spawnPoint.Rotation).GetComponent<T>();
    }

    private SpawnPoint GetSpawnPointFromSpawnType(SpawnType spawnType)
    {
        return _spawnPoints.Find(item => item.SpawnType == spawnType);
    }
}