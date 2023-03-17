using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void QuitButtonClick()
    {
        Application.Quit();
    }

    public void LevelButtonClick(int level)
    {
        SceneManager.LoadScene(level);
    }

}
