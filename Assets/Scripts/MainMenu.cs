using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Canvas MainMenuScreen;
    public Canvas HowToPlayScreen;
    public Canvas LevelSelectScreen;

    public int level = 1;

    void Awake()
    {
        LevelSelectScreen.enabled = false;
        HowToPlayScreen.enabled = false;
    }

    public void LevelSelectOn()
    {
        LevelSelectScreen.enabled = true;
        HowToPlayScreen.enabled = false;
        MainMenuScreen.enabled = false;
    }

    public void HowToPlayOn()
    {
        HowToPlayScreen.enabled = true;
        LevelSelectScreen.enabled = false;
        MainMenuScreen.enabled = false;
    }

    public void ReturnOn()
    {
        MainMenuScreen.enabled = true;
        HowToPlayScreen.enabled = false;
        LevelSelectScreen.enabled = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //level swap
    public void Level2()
    {
        level = 2;
        SceneManager.LoadScene(level);
    }

    public void Level3()
    {
        level = 3;
        SceneManager.LoadScene(level);
    }

    public void Level4()
    {
        level = 4;
        SceneManager.LoadScene(level);
    }

    public void Level5()
    {
        level = 5;
        SceneManager.LoadScene(level);
    }
}
