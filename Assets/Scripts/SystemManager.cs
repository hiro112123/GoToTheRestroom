/*
    シーンの遷移
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    public void GameStart()
    {
        //シーンを読み込む
        SceneManager.LoadScene(1);
    }

    public void GoToTown()
    {
        SceneManager.LoadScene(2);
    }


    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
