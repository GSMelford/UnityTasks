using UnityEngine;

public class Turret : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Hero hero = other.gameObject.GetComponent<Hero>();

        if (hero != null)
        {
            hero.InteractionWithTurret();
            Destroy(gameObject);
        }
    }
}
