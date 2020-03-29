using System;
using Generated;
using Plugins.Timer.Source;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class BasesManager : MonoBehaviour
    {
        private Base.Base[] _bases;
        private void Awake()
        {
            MyGameManager.Instance.SpawnedObjects += OnSpawnedObjects;
        }

        private void OnSpawnedObjects()
        {
            _bases = FindObjectsOfType<Base.Base>();
        }

        private void Update()
        {
            bool existsBase = false;
            foreach (var baseStructure in _bases)
            {
                if (baseStructure != null)
                {
                    existsBase = true;
                }
            }

            if (!existsBase)
            {
                Timer.Register(2, () => SceneManager.LoadScene(Scenes.WINSCENE));
            }
        }
    }
}