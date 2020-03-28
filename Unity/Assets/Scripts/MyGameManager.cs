using System;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private void Start()
    {
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Tower);
        SpawnManager.Instance.SpawnAllWithType(SpawnType.Bomb);
    }
}