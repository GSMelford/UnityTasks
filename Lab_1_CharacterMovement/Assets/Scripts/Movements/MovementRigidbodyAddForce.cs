using UnityEngine;

namespace Movements
{
    public class MovementRigidbodyAddForce : IMovement
    {
        private readonly Rigidbody2D _rigidbody2D;

        public MovementRigidbodyAddForce(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }
        
        public void DoMovement(float direction, float speed)
        {
            if (direction < 0)
            {
                _rigidbody2D.AddForce(Vector2.left * speed);
            }
            else if (direction > 0)
            {
                _rigidbody2D.AddForce(Vector2.right * speed);
            }
        }
    }
}