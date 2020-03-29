using System;
using Gamelogic.Extensions;
using UnityEngine;

public class MyGameManager : Singleton<MyGameManager>
{
    [SerializeField]
    private BombCounter _bombCounter;

    [SerializeField] 
    private HealthUIScript _healthUiScript;

    [SerializeField] 
    private Transform _smokeBarTransform;

    public BombCounter BombCounter => _bombCounter;

    public HealthUIScript HealthUiScript => _healthUiScript;

    public Transform SmokeBarTransform => _smokeBarTransform;
    
    private void Start()
    {
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Tower);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Bomb);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Owl);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Base);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Ground);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.CraterGround);
    }
}