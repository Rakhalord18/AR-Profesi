using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Choice Character");
    }

    public void CharacterTNIPria()
    {
        SceneManager.LoadScene("Character");
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
    }

        public void QuitGame()
    {
        Application.Quit();
    }
}
