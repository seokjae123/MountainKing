using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void gameStart()
    {
        SceneManager.LoadScene("02.Stage_1");
    }

    public void credit() 
    {
        //SceneManager.LoadScene("02.Credit");
    }

    public void gameExit()
    {
        Application.Quit();
    }
}
