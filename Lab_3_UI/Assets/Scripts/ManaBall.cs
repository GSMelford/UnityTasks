using UnityEngine;

public class ManaBall : MonoBehaviour
{
    [SerializeField] private int ManaBonus = 40;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Hero hero = other.gameObject.GetComponent<Hero>();

        if (hero != null)
        {
            hero.AddMana(ManaBonus);
            Destroy(gameObject);
        }
    }
}
