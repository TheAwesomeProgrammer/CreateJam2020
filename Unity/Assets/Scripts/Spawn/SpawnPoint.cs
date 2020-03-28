using System;
using Gamelogic.Extensions;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnPoint : MonoBehaviour
{
    private SpawnType _lastSpawnType;
    
    [SerializeField]
    private SpawnType spawnType;

    public SpawnType SpawnType => spawnType;

    public Vector2 Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    public Vector3 Scale => transform.localScale;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            ClearSpawnedObject();
        }
    }

    private void ClearSpawnedObject()
    {
        transform.DestroyChildrenUniversal();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (_lastSpawnType != spawnType || transform.childCount <= 0)
            {
                ClearSpawnedObject();
                GameObject spawnedGo = SpawnManager.Instance.Spawn(this);
                spawnedGo.transform.parent = transform;
            }

            _lastSpawnType = spawnType;
        }
    }
}