using UnityEngine;
using Yurowm.DebugTools;

namespace Common.Menu
{
    [ExecuteInEditMode]
    public class BackButtonScenesItemManager : MenuItemManagerBase
    {
        [SerializeField]
        private MenuItem _backButton;
        
        protected override void UpdateMenuItems(MenuSprites menuSprites)
        {
            _backButton.UpdateItemIfSpriteNotNull(menuSprites.BackButtonSprite);
        }

        protected override bool HasAllSpritesBeenSet(MenuSprites menuSprites)
        {
            return _backButton && menuSprites.BackButtonSprite;
        }
    }
}