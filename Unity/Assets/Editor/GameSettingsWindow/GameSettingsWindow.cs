using Editor.GameSettingsWindow;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class GameSettingsWindow : EditorWindow
{
    [MenuItem("Tools/GameSettingsWindow")]
    public static void ShowExample()
    {
        GameSettingsWindow wnd = GetWindow<GameSettingsWindow>();
        wnd.titleContent = new GUIContent("GameSettingsWindow");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/GameSettingsWindow/GameSettingsWindow.uxml");
        VisualElement labelFromUXML = visualTree.CloneTree();
        root.Add(labelFromUXML);

        VisualElement rightPanel = root.Q<VisualElement>("RightPanel");
        PlayerStatsEditorUI playerStatsEditorUi = new PlayerStatsEditorUI("Assets/Editor/GameSettingsWindow/Stats/Player/PlayerStatsEditorUI.uxml", 
            "Data/Player/ExamplePlayer");
        rightPanel.Add(playerStatsEditorUi.CreateEditorUi());

        VisualElement leftPanel = root.Q<VisualElement>("LeftPanel");
        VisualElement selectLabel = SelectLabel.Create("This/is/a/test", path => Debug.Log("Path clicked: " + path));
        leftPanel.Add(selectLabel);
        
        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var gameSettingsStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/GameSettingsWindow/GameSettingsWindow.uss");
        root.styleSheets.Add(gameSettingsStyleSheet);
        var statsTemplateStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/GameSettingsWindow/Stats/StatsTemplate.uss");
        root.styleSheets.Add(statsTemplateStyleSheet);
    }
    
    private void OnGUI()
    {
        // Set the container height to the window
        rootVisualElement.Q<VisualElement>("Container").style.height = new 
            StyleLength(position.height);
    }
}