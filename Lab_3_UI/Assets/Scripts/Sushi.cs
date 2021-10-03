using UnityEngine;
using UnityEngine.SceneManagement;

public class Sushi : MonoBehaviour
{
    [SerializeField] private int HpBonus = 30;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Hero hero = other.gameObject.GetComponent<Hero>();

        if (hero != null)
        {
            hero.HpUp(HpBonus);
            Destroy(gameObject);
        }
    }
}