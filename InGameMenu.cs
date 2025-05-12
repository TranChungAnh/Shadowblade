using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("GameStart");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
