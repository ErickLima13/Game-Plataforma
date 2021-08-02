using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject infoObj;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Info()
    {
        infoObj.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
