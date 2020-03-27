using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Menu
{
    public abstract class MenuItemManagerBase : MonoBehaviour
    {
        [SerializeField]
        private MenuSprites _menuSprites;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private GameObject _templateRootGo;

        [SerializeField] 
        private GameObject _menuRootGo;

        private void Update()
        {
            if (Application.isEditor)
            {
                if (HasAllSpritesBeenSet(_menuSprites))
                {
                    _templateRootGo.SetActive(false);
                    _menuRootGo.SetActive(true);
                }
                else
                {
                    _templateRootGo.SetActive(true);
                    _menuRootGo.SetActive(false);
                }

                UpdateMenuitem();
            }
        }

        private void UpdateMenuitem()
        {
            UpdateImageIfSpriteNotNull(_background, _menuSprites.BackgroundSprite);
            UpdateMenuItems(_menuSprites);
        }

        private void UpdateImageIfSpriteNotNull(Image image, Sprite sprite)
        {
            if (image != null && sprite != null)
            {
                image.sprite = sprite;
            }
        }

        protected abstract void UpdateMenuItems(MenuSprites menuSprites);
        
        protected abstract bool HasAllSpritesBeenSet(MenuSprites menuSprites);
    }
}