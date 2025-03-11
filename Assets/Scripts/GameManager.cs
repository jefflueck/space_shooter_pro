using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            // Restart the game
            SceneManager.LoadScene(1); // current game scene
        }

        // if escape key is pressed
        // quit the application
        if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == true)
        {
            Application.Quit();
            SceneManager.LoadScene(0); // main menu scene
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

}


