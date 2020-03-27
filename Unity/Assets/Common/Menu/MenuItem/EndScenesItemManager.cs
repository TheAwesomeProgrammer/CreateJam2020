using UnityEngine;
using UnityEngine.UI;

namespace Common.Menu
{
    [ExecuteInEditMode]
    public class EndScenesItemManager : MenuItemManagerBase
    {
        [SerializeField]
        private MenuItem _restartMenuItem;

        [SerializeField]
        private MenuItem _menuMenuItem;
        
        protected override void UpdateMenuItems(MenuSprites menuSprites)
        {
            GameEndScenesSprites gameEndScenesSprites = menuSprites.GameEndScenesSprites;
            _restartMenuItem.UpdateItemIfSpriteNotNull(gameEndScenesSprites.RestartButtonSprite);
            _menuMenuItem.UpdateItemIfSpriteNotNull(gameEndScenesSprites.MenuButtonSprite);
        }
        
        protected override bool HasAllSpritesBeenSet(MenuSprites menuSprites)
        {
            GameEndScenesSprites gameEndScenesSprites = menuSprites.GameEndScenesSprites;
            return gameEndScenesSprites.RestartButtonSprite != null &&
                   gameEndScenesSprites.MenuButtonSprite != null;
        }
    }
}