using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    // Called when we click the "Play" button.
    public void OnHomeButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }

    // Called when we click the "Quit" button.

}

