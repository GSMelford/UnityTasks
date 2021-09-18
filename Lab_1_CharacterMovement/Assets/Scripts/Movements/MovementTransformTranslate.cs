using UnityEngine;

namespace Movements
{
    public class MovementTransformTranslate : IMovement
    {
        private readonly Transform _transform;
        
        public MovementTransformTranslate(Transform transform)
        {
            _transform = transform;
        }
        
        public void DoMovement(float direction, float speed)
        {
            if (direction < 0)
            {
                _transform.Translate(Vector3.left * Time.deltaTime);
            }
            else if (direction > 0)
            {
                _transform.Translate(Vector3.right * Time.deltaTime);
            }
        }
    }
}