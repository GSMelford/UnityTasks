using UnityEngine;

public class Drone : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other)
   {
      Hero hero = other.gameObject.GetComponent<Hero>();

      if (hero != null)
      {
         hero.DoHurt();
         Destroy(gameObject);
      }
   }
}
