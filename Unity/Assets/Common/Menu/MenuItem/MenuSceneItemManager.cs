using UnityEngine;
using UnityEngine.UI;

namespace Common.Menu
{
    [ExecuteInEditMode]
    public class MenuSceneItemManager : MenuItemManagerBase
    {
        [SerializeField]
        private MenuItem _playMenuItem;
        
        [SerializeField]
        private MenuItem _creditsMenuItem;
        
        [SerializeField]
        private MenuItem _tutorialMenuItem;
        
        [SerializeField]
        private MenuItem _exitMenuItem;

        protected override void UpdateMenuItems(MenuSprites menuSprites)
        {
            MenuSceneSprites menuSceneSprites = menuSprites.MenuSceneSprites;
            _playMenuItem.UpdateItemIfSpriteNotNull(menuSceneSprites.PlaySprite);
            _creditsMenuItem.UpdateItemIfSpriteNotNull(menuSceneSprites.CreditsSprite);
            _tutorialMenuItem.UpdateItemIfSpriteNotNull(menuSceneSprites.TutorialSprite);
            _exitMenuItem.UpdateItemIfSpriteNotNull(menuSceneSprites.ExitSprite);
        }

        protected override bool HasAllSpritesBeenSet(MenuSprites menuSprites)
        {
            MenuSceneSprites menuSceneSprites = menuSprites.MenuSceneSprites;
            return menuSceneSprites.CreditsSprite != null &&
                   menuSceneSprites.PlaySprite != null &&
                   menuSceneSprites.ExitSprite != null &&
                   menuSceneSprites.TutorialSprite != null;
        }
    }
}