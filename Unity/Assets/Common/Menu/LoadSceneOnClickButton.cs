using System.IO;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Menu
{
    [ExecuteInEditMode]
    public class LoadSceneOnClickButton : ButtonBase
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
    
        private void Update()
        {
            if (Application.isEditor)
            {
                _allScenes = GetAllScenes();
            }
        }
        protected override void OnClick()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}
