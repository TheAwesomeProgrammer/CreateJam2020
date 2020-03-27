using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.GameSettingsWindow
{
    public delegate void SelectLabelCallback(string path);
    
    public class SelectLabel
    {
        private const char PATH_DELIMITER = '/';
        
        private static SelectLabelCallback _onClickedAction;
        
        public static VisualElement Create(string selectPath, SelectLabelCallback onClicked)
        {
            _onClickedAction = onClicked;

            return CreateFoldoutWithSubPaths(selectPath);
        }

        private static VisualElement CreateFoldoutWithSubPaths(string path)
        {
            string[] paths = path.Split(PATH_DELIMITER);
            int rootPathIndex = 0;
            VisualElement rootPathElement = CreateElementForPath(rootPathIndex, paths[rootPathIndex], paths.Length);
            AddSubPathsToRootPathElement(paths, rootPathElement, rootPathIndex);

            return rootPathElement;
        }

        private static void AddSubPathsToRootPathElement(string[] paths, VisualElement rootPathElement, int rootPathIndex)
        {
            VisualElement currentPathElement = rootPathElement;
            
            for (int i = rootPathIndex + 1; i < paths.Length; i++)
            {
                VisualElement elementForCurrentPath = CreateElementForPath(i, paths[i], paths.Length);
                currentPathElement.Add(elementForCurrentPath);

                currentPathElement = elementForCurrentPath;
            }
        }

        private static VisualElement CreateElementForPath(int index, string path, int pathLength)
        {
            bool isAtEndOfPath = index + 1 == pathLength;
            
            if (isAtEndOfPath)
            {
                return CreateLabelWithCallback(path);
            }

            return CreateFoldout(path);
        }
        
        private static VisualElement CreateLabelWithCallback(string labelName)
        {
            Label label = new Label(labelName);
            label.AddToClassList(UssClasses.MIDDLE_LEFT_TEXT_ALIGN);
            label.RegisterCallback<MouseDownEvent>(evt => _onClickedAction(labelName));
            return label;
        }

        private static Foldout CreateFoldout(string path)
        {
            Foldout foldout = new Foldout()
            {
                text = path
            };
            return foldout;
        }

    }
}