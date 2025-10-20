using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public PlayerMove playerMoveScript; // assign this in the inspector

    public void TitleScreen()
    {
        Debug.Log("Loading Title Screen...");

        if (playerMoveScript != null)
            playerMoveScript.Freeze();

        SceneManager.LoadScene("Title");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void CreditScreen()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ControlsScreen()
    {
        SceneManager.LoadScene("ControlScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
