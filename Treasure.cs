using UnityEngine;

public class Treasure : MonoBehaviour
{
    public AudioSource collision;
    public int points = 1; // Points to add when collected
    private void Start()
    {
        collision = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
            collision.Play();
            Debug.Log("audio played");
   
        GameManager gameManager = FindObjectOfType<GameManager>(); // Find the GameManager
             if (gameManager != null)
             {
                 gameManager.AddScore(points); // Add points to the score
               

            }
            Destroy(gameObject); // Remove the treasure

        }
    }
}
