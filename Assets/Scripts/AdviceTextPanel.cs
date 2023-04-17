/*
    ゲームクリア、ゲームオーバーになった時
    パネルを表示してテキストを表示する
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdviceTextPanel : MonoBehaviour
{
    //パネル
    public GameObject advicePanel;
    //GameOverの場合のテキスト
    public GameObject gameOverAdviceText;
    //GameClearの場合のテキスト
    public GameObject gameClearAdviceText;
    //走るボタン
    public GameObject runButton;
    //しゃがむボタン
    public  GameObject squatButton;
    // Start is called before the first frame update
    void Start()
    {
        //パネルの非表示
        advicePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //GameOverだったときに表示させる
        if(GameState.GameOver)
        {
            Invoke("ActiveGameOverAdviceText", 5);
            return;
        }
        else if(GameState.GameClear)
        {
            Invoke("ActiveGameClearAdviceText", 5);
            return;
        }
    }

    //GameOverの時にパネルを表示しテキストなどの表示や非表示
    public void ActiveGameOverAdviceText()
    {
        //パネルの表示
        advicePanel.SetActive(true);
        //GameOverTextの非表示
        //gameOverText.SetActive(false);
        //GameClearAdviceTextの非表示
        gameClearAdviceText.SetActive(false);
        //各ボタンの非表示
        runButton.SetActive(false);
        squatButton.SetActive(false);
        //GameOver AdviceTextの表示
        gameOverAdviceText.SetActive(true);
    }

    //GameClearの時にパネルを表示しテキストなどの表示や非表示
    public void ActiveGameClearAdviceText()
    {
        //パネルの表示
        advicePanel.SetActive(true);
        //GameClearTextの非表示
        //gameClearText.SetActive(false);
        //GameOverAdviceTextの非表示
        gameOverAdviceText.SetActive(false);
        //各ボタンの非表示
        runButton.SetActive(false);
        squatButton.SetActive(false);
        //GameClearAdviceTextの表示
        gameClearAdviceText.SetActive(true);
    }
}
