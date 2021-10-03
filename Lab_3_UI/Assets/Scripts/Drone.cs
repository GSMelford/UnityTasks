using UnityEngine;

public class Drone : MonoBehaviour
{
   [SerializeField] private int DamageValue = 20;
   
   private void OnCollisionEnter2D(Collision2D other)
   {
      Hero hero = other.gameObject.GetComponent<Hero>();

      if (hero != null)
      {
         hero.DoHurt(DamageValue);
         Destroy(gameObject);
      }
   }
}
