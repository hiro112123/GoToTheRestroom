/*
    ゲーム内のタイムリミットについて
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLimit : MonoBehaviour
{
    //タイムリミットのゲージの変数
    public Image timeLimitGauge;
    //タイムリミットの秒数
    public float timeLimitSeconds = 30f;
    //時間経過
    public float timeGauge;
    //ゲームオーバーのテキスト
    public GameObject gameOverText;


    void Start()
    {
        //テキストの非表示
        gameOverText.SetActive(false);
    }
    void Update()
    {
        //GameOverだったときに表示させる
        // if(GameState.GameOver)
        // {
        //     return;
        // }
        if(SceneManager.GetActiveScene().name == "Home")
        {
            if(timeGauge > 15)
        {
            gameOverText.SetActive(true);
            GameState.GameOver = true;
            return;
        }
        }

        //30秒超えたらテキストの表示とゲーム状態の変更
        if(timeGauge > 30)
        {
            gameOverText.SetActive(true);
            GameState.GameOver = true;
            return;
        }

        if(!GameState.GameClear)
        {
            //カウントダウンの関数
            CountTimer();
        }
        
    }

    //カウントダウン
    public void CountTimer()
    {
        //経過した時間を追加
        //走っていたら時間経過を早くする
        if(RunAndSquat.running)
        {
            timeGauge += Time.deltaTime * 2.0f;
        }
        //しゃがんでいたら経過時間をもどす
        else if(!RunAndSquat.standing && timeGauge > 0)
        {
            timeGauge -= Time.deltaTime * 0.5f;
        }
        else
        {
            timeGauge += Time.deltaTime;
        }
        
        //経過した時間に合わせゲージを動かす
        //街の場合と家の場合でタイムリミットのゲージの時間を調整する
        if(SceneManager.GetActiveScene().name == "Home")
        {
            timeLimitGauge.rectTransform.localScale = new Vector3(timeGauge * 2 / timeLimitSeconds, 1, 1);
        }
        else
        {
            timeLimitGauge.rectTransform.localScale = new Vector3(timeGauge/ timeLimitSeconds, 1, 1);
        }
        
        //Debug.Log(timeGauge);
    }
}
