using UnityEngine;

public class MagicBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Hero hero = other.gameObject.GetComponent<Hero>();

        if (hero != null)
        {
            hero.InteractionWithMagicBall();
            Destroy(gameObject);
        }
    }
}
