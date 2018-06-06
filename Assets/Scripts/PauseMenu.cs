using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public bool isPaused;
    public Canvas PauseMenuCanvas;
	
	// Update is called once per frame
	void Update () {

        if (isPaused)
        {
            PauseMenuCanvas.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            PauseMenuCanvas.enabled = false;
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
	
	}

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        isPaused = false;
        SceneManager.LoadScene(1);
    }
}
