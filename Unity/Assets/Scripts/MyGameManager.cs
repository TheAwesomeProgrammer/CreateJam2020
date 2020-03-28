using System;
using Gamelogic.Extensions;
using UnityEngine;

public class MyGameManager : Singleton<MyGameManager>
{
    [SerializeField]
    private BombCounter _bombCounter;

    public BombCounter BombCounter => _bombCounter;
    
    private void Start()
    {
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Tower);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Bomb);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Owl);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Base);
    }
}