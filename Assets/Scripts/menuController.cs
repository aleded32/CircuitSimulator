using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void startBtn()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void quitBtn()
    {
        Application.Quit();
    }

    public void continueBtn()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void quitToMainBtn()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}