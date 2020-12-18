using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject Main;
    public GameObject Credit;
    public GameObject Tut;
    


    public void ExitGame()
    {

        Application.Quit();
    
    }

    public void PlayGame()
    {

        SceneManager.LoadScene("Implementation");
    
    }

    public void GoToTut()
    {

        Main.SetActive(false);
        Tut.SetActive(true);

    }

    public void CreditsScreen()
    {

        Main.SetActive(false);
        Credit.SetActive(true);

    }
    public void MainMenuScreen()
    {

        Main.SetActive(true);
        Credit.SetActive(false);

    }

}
