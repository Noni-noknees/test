using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float health = 50f;
    public Slider healthBar;

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health/100;

        if (health <= 0)
        {
            SceneManager.LoadScene(3);
            Debug.Log("Player has died!");
            // Implement player death logic, like restarting the game or showing a game over screen.
        }
    }
    private void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
