using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    

    public GameObject menuUi;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (globalData.GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        menuUi.SetActive(false); 
        globalData.GamePaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        menuUi.SetActive(true);
        globalData.GamePaused = true;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading main menu...");
        SceneManager.LoadScene("LoginScene/LoginGAME", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
