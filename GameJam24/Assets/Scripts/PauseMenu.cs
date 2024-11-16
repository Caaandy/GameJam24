using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void GoBackToMenu() 
    {
        SceneManager.LoadScene(0);
    }
}
