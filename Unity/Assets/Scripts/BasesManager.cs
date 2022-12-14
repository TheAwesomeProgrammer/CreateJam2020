using System;
using System.IO;
using Generated;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using Plugins.Timer.Source;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    [ExecuteInEditMode]
    public class BasesManager : MonoBehaviour
    {
        private DropdownList<string> _allScenes;

        [SerializeField, Dropdown("_allScenes")] 
        private string _sceneToLoad;

        private DropdownList<string> GetAllScenes()
        {
            DropdownList<string> allScenes = new DropdownList<string>();
#if UNITY_EDITOR
            foreach (var scene in EditorBuildSettings.scenes)
            {
                allScenes.Add(Path.GetFileNameWithoutExtension(scene.path), Path.GetFileNameWithoutExtension(scene.path));
            }
            #endif

            return allScenes;
        }
        
        private Base.Base[] _bases;
        private void Awake()
        {
            if (Application.isPlaying)
            {
                MyGameManager.Instance.SpawnedObjects += OnSpawnedObjects;
            }

        }

        private void OnSpawnedObjects()
        {
            _bases = FindObjectsOfType<Base.Base>();
        }

        private void Update()
        {
            if (Application.isPlaying)
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
                    Timer.Register(2, () => SceneManager.LoadScene(_sceneToLoad));
                }
            }
            else if (!Application.isPlaying)
            {
                _allScenes = GetAllScenes();
            }
        }
    }
}