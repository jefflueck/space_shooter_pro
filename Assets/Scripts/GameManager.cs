using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    // check if in co-op mode or single player mode
    [SerializeField]
    public bool _isCoOpMode = false;
    private SpawnManager _spawnManager;


    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            // Restart the game with current selected scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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


