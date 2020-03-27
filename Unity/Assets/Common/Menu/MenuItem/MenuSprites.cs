using System;
using UnityEngine;

namespace Common.Menu
{
    [Serializable][CreateAssetMenu(fileName = "MenuItemSprites", menuName = "Menu system/Menu item sprites", order = 56)]
    public class MenuSprites : ScriptableObject
    {
        public Sprite BackButtonSprite;
        public Sprite BackgroundSprite;
        public MenuSceneSprites MenuSceneSprites;
        public GameEndScenesSprites GameEndScenesSprites;

    }
    [Serializable]
    public class MenuSceneSprites
    {
        public Sprite PlaySprite;
        public Sprite CreditsSprite;
        public Sprite TutorialSprite;
        public Sprite ExitSprite;
    }

    [Serializable]
    public class GameEndScenesSprites
    {
        public Sprite RestartButtonSprite;
        public Sprite MenuButtonSprite;
    }
    
}