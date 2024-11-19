using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TMPro.TMP_Text scoreText;

    void Start()
    {
        UpdateScoreText(); // Initialize the score text at the start
    }

    public void AddScore(int points)
    {
        score += points; // Add points to the score
        UpdateScoreText(); // Update the displayed score
        if (score >= 15)
        {
            SceneManager.LoadScene(4);
        }
    }
 

    void UpdateScoreText()
    {
        scoreText.text = "Pages: " + score; // Update the score display
    }
   
}

