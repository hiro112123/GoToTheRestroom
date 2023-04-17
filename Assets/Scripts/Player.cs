/*
    プレイヤーの動きや視点の操作
    ボタンを押した際の動き
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //ゲームクリアのテキスト
    public GameObject gameClearText;
    //トイレットペーパーのテキスト
    public GameObject toiletPaperText;
    //持っているトイレットペーパー
    public GameObject toiletPaper;
    //トイレットペーパーを持っているかどうか
    public bool haveToiletPaper = false;
    //音が出るスピーカー
    public AudioSource gameSound;
    //音が出る音源
    public AudioClip toiletSE, stomachSE;
    //ドアボタン
    public GameObject doorButton;

    private void Start()
    {
        //ゲーム状態の変更
        GameState.GameOver = false;
        GameState.GameClear = false;

        //テキストの非表示
        gameClearText.SetActive(false);
        toiletPaperText.SetActive(false);

        //オブジェクトの非表示
        toiletPaper.SetActive(false);

        //お腹の音を鳴らす
        gameSoundPlay(stomachSE);

        //ドアのボタンを消しとく
        doorButton.SetActive(false);

    }

    private void Update()
    {
        //ゲームオーバーまたはゲームクリアの場合動かないようにする
        if(GameState.GameOver || GameState.GameClear)
        { 
            return;
        }
        
        //動くべき方向を変数に格納
        Vector3 moveDir = ((transform.forward * JoyStick_Move.joyStickPosY) +
            (transform.right * JoyStick_Move.joyStickPosX)).normalized;

        //playerの高さの更新
        Vector3 playerScale = new Vector3(1, RunAndSquat.playerHeight, 1);
        transform.localScale = playerScale;
        
        //しゃがんでいたら歩けない
        if(RunAndSquat.standing)
        {
            //ポジション更新
            transform.position += moveDir * RunAndSquat.speed * Time.deltaTime;
        }
        
        //回転を更新する
        transform.rotation = Quaternion.Euler(JoyStick_Cam.rotY, JoyStick_Cam.rotX, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //タグがToiletだったら、テキスト表示とゲーム状態の変更
        if(other.tag == "Toilet")
        {
            if(haveToiletPaper)
            {
                gameClearText.SetActive(true);
                gameSoundPlay(toiletSE);
                GameState.GameClear = true;
            }
            else
            {
                toiletPaperText.SetActive(true);
                Invoke("DestroyText", 3);
            }
        }
        //タグがPaperだったら、フラグを変更とオブジェクトを消す、目の前にオブジェクトの表示
        else if(other.tag == "Paper")
        {
            haveToiletPaper = true;
            Destroy(other.gameObject);
            toiletPaper.SetActive(true);

        }
        //玄関に着いたらシーンを変える
        else if(other.tag == "Entrance")
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        //ドア付近にいる時だけボタンを表示する
        if(other.gameObject.name == "Door")
        {
            doorButton.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //ドアから離れたらボタンを消す
        if(other.gameObject.name == "Door")
        {
            doorButton.SetActive(false);
        }
    }

    //テキストの非表示
    void DestroyText()
    {
        toiletPaperText.SetActive(false);
    }

    //音を出す
    public void gameSoundPlay(AudioClip clip)
    {
        //引数をスピーカーにセット
        gameSound.clip = clip;
        //音を出す
        gameSound.Play();
    }
}
