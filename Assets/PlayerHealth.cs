using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   // The health of the player
   public float health;

   // The damage effect that will be shown when the player takes damage
   public GameObject damageEffect;

   // The menu handler script, which is used to show the game over menu when the player dies
   public MenuHandler menuHandler;

   // This method is called when the player takes damage
   public void TakeDamage(float damage)
   {
      StartCoroutine(TakeDamageCo(damage));  
   }

   // This method handles the player taking damage,
   // and shows the damage effect when the player takes damage,
   // and shows the game over menu when the player dies.
   IEnumerator TakeDamageCo(float damage)
   {
      yield return new WaitForSeconds(1f);
      health -= damage;

      // Show the damage effect when the player takes damage, which is a red screen effect 
      damageEffect.SetActive(true);

      if (health <= 0)
      {
         menuHandler.GameOver();
      }

      // Hide the damage effect after 1 second
      yield return new WaitForSeconds(1f);
      damageEffect.SetActive(false);
   }
}
