using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Movement
{
    public class PlayerMovement : Movement
    {
        private Vector2 _moveDirection;
        
        public PlayerMovement(MovementSetup movementSetup, MovementStats movementStats) : base(movementSetup, movementStats)
        {
        }

        public void OnMove(Vector2 direction)
        {
            _moveDirection = direction;
        }

        protected override Vector2 GetMoveDirection()
        {
            return _moveDirection;
        }
    }
}