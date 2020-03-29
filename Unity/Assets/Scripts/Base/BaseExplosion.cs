using System;
using UnityEngine;

namespace Base
{
    public class BaseExplosion : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D[] _children;

        [SerializeField] 
        private float _explosionForce;

        [SerializeField] 
        private float _explosionRadius;
        
        private void Awake()
        {
            foreach (var child in _children)
            {
                child.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }
}