using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int CoinValue = 100;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Hero hero = other.gameObject.GetComponent<Hero>();

        if (hero != null)
        {
            hero.AddCoin(CoinValue);
            Destroy(gameObject);
        }
    }
}
