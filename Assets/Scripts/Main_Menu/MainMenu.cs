using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        SceneManager.LoadScene(1); // "Single_Player" scene
    }

    public void LoadCoOpGame()
    {
        SceneManager.LoadScene(2); // "Co-Op" scene
    }

}
