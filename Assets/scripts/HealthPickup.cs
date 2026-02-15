using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1; // Bu kalp kaç can verecek?

    private void OnTriggerEnter2D(Collider2D other)
    {
 
        if (other.CompareTag("Player"))
        {
            
            Entity playerEntity = other.GetComponent<Entity>();

            if (playerEntity != null)
            {
               
                playerEntity.Heal(healAmount);

                Destroy(gameObject);
            }
        }
    }
}