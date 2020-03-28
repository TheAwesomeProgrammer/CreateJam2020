using System;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private void Start()
    {
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Tower);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Bomb);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Owl);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Base);
    }
}