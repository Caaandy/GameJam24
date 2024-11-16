using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu: MonoBehaviour 
{
    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

