using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Victory: MonoBehaviour
{
    // Called when we click the "Play" button.
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    // Called when we click the "Quit" button.

}

