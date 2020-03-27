using UnityEngine;

namespace Common.UnitSystem
{
    public class GraphicUnitSetup : UnitSetup
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] 
        private Transform _graphicsTransform;
        public Transform GraphicsTransform => _graphicsTransform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}