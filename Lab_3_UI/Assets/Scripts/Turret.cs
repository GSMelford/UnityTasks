using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
    [SerializeField] private int SceneNumber = 1;
    [SerializeField] private int CoinValue = 300;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Hero hero = other.gameObject.GetComponent<Hero>();

        if (hero != null)
        {
            hero.InteractionWithTurret();

            if (hero.CoinValue >= CoinValue)
            {
                Invoke(nameof(LoadNextScene), 1f);
            }
        }
    }
    
    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
