using UnityEngine;

namespace Movements
{
    public class MovementRigidbodyVelocity
    {
        private readonly Rigidbody2D _rigidbody2D;

        public MovementRigidbodyVelocity(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }
        
        public void DoMovement(float direction, float speed)
        {
            _rigidbody2D.velocity = new Vector2(direction * speed, _rigidbody2D.velocity.y);
        }
    }
}