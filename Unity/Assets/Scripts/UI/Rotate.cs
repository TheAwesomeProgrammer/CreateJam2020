using System;
using UnityEngine;

namespace UI
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField]
        private float _rotateSpeed;
        
        private void Awake()
        {
            transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
        }
    }
}