using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class PlayerStatsEditorUI
{
    private string _pathToUXML;
    private string _pathToStatsManager;
    
    public PlayerStatsEditorUI(string pathToUXML, string pathToStatsManager)
    {
        _pathToUXML = pathToUXML;
        _pathToStatsManager = pathToStatsManager;
    }
    
    public VisualElement CreateEditorUi()
    {
        ExamplePlayerStatsManager examplePlayerStatsManager =
            Resources.Load<ExamplePlayerStatsManager>(_pathToStatsManager);
        var playerStatsVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(_pathToUXML);
        VisualElement playerStatsRoot = playerStatsVisualTree.CloneTree();
        SerializedObject serializedStatsManagerObject = new SerializedObject(examplePlayerStatsManager);
        
        SetupStats(playerStatsRoot, serializedStatsManagerObject, new SetupStatData()
        {
            StatsContainerPropertyName = "_healthStats",
            RootFoldoutName = "health-stats",
            StatProperties = new []
            {
                new StatProperty
                {
                    Name = "_healthStat",
                    Text = "Health"
                }, 
                new StatProperty
                {
                    Name = "_invulnerabilityDuration",
                    Text = "Invulnerability duration"
                }, 
            }
        });
        
        SetupStats(playerStatsRoot, serializedStatsManagerObject, new SetupStatData()
        {
            StatsContainerPropertyName = "_movementStats",
            RootFoldoutName = "movement-stats",
            StatProperties = new []
            {
                new StatProperty
                {
                    Name = "_speed",
                    Text = "Speed "
                }, 
                new StatProperty
                {
                    Name = "_maxSpeed",
                    Text = "Max speed"
                }, 
            }
        });

        return playerStatsRoot;
    }

    private void SetupStats(VisualElement statsRootElement, SerializedObject serializedStatObj, SetupStatData setupStatData)
    {
        SerializedProperty statsContainerProperty = serializedStatObj.FindProperty(setupStatData.StatsContainerPropertyName);
        Foldout rootFoldout = statsRootElement.Q<Foldout>(setupStatData.RootFoldoutName);
        foreach (var statProperty in setupStatData.StatProperties)
        {
            SetupStat(statsContainerProperty.FindPropertyRelative(statProperty.Name), rootFoldout, statProperty);
        }
    }

    private void SetupStat(SerializedProperty serializedStatProperty, Foldout parentStatsElement, StatProperty statProperty)
    {
        var statsVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/GameSettingsWindow/Stats/StatsTemplate.uxml");
        VisualElement statsRoot = statsVisualTree.CloneTree();
        
        SetupStatValues(statsRoot, serializedStatProperty);
        HideMaxMinValuesIfNeeded(statsRoot, serializedStatProperty);
        SetRootFoldoutText(statsRoot, statProperty);

        parentStatsElement.Add(statsRoot);
    }

    private void SetupStatValues(VisualElement statsRoot, SerializedProperty serializedStatProperty)
    {
        SetupStatProperty<FloatField>(serializedStatProperty, statsRoot, "start-value", "_startStat");
        SetupStatProperty<FloatField>(serializedStatProperty, statsRoot, "max-value", "_maxStatValue");
        SetupStatProperty<FloatField>(serializedStatProperty, statsRoot, "min-value", "_minStatValue");
    }
    private void SetupStatProperty<T>(SerializedProperty serializedStatProperty, VisualElement statsRoot, string statValueUiName, string statPropertyName) where T : BindableElement
    {
        T startValueField = statsRoot.Q<T>(statValueUiName);
        startValueField.BindProperty(serializedStatProperty.FindPropertyRelative(statPropertyName));
    }
    
    private void HideMaxMinValuesIfNeeded(VisualElement statsRoot, SerializedProperty serializedStatProperty)
    {
        bool shouldHideMaxMinValues = !serializedStatProperty.FindPropertyRelative("_showMinMaxValue").boolValue;
        VisualElement maxMinValuesContainer = statsRoot.Q<VisualElement>("max-min-values-container");
        if (shouldHideMaxMinValues)
        {
            maxMinValuesContainer.RemoveFromHierarchy();
        }
    }

    private void SetRootFoldoutText(VisualElement statsRoot, StatProperty statProperty)
    {
        Foldout statTemplateRoot = statsRoot.Q<Foldout>("stats-root-foldout");
        statTemplateRoot.text = statProperty.Text;
    }
    
    public class SetupStatData
    {
        public string StatsContainerPropertyName;
        public string RootFoldoutName;
        public StatProperty[] StatProperties;
    }
    
    public class StatProperty
    {
        public string Name;
        public string Text;
    }
}