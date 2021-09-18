using UnityEngine;

namespace Movements
{
    public class MovementTransform : IMovement
    {
        private readonly Transform _transform;
        
        public MovementTransform(Transform transform)
        {
            _transform = transform;
        }
        
        public void DoMovement(float direction, float speed)
        {
            if (direction < 0)
            {
                ChangeX(-speed);
            }
            else if (direction > 0)
            {
                ChangeX(speed);
            }
        }

        private void ChangeX(float speed)
        {
            Vector2 position = _transform.position;
            position = new Vector2(position.x + speed, position.y);
            _transform.position = position;
        }
    }
}