/*
    画面の左側をタップやスライドをするとジョイスティックが表示される
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick_Move : MonoBehaviour
{
    //変数
    //スティック格納
    public GameObject joyStick;
    //ジョイスティックキャンバスのポジション
    private RectTransform joyStickRectTransform;
    //ジョイスティックの後ろの丸
    public GameObject backGround;
    //スティックが動ける範囲
    public int stickRange = 3;
    //実際に動く値
    private int stickMovement = 0;
    //プレイヤーを動かすための変数
    public static float joyStickPosX;
    public static float joyStickPosY;

    private void Start()
    {
        //初期設定
        Initialization(); 
    }

    //初期設定
    private void Initialization()
    {
        //違う画面サイズでも似たような挙動にするため
        stickMovement = stickRange * (Screen.width + Screen.height) / 100;
        //ジョイスティックのRectTransformを取得する
        joyStickRectTransform = joyStick.GetComponent<RectTransform>();
        //ジョイスティックの非表示
        JoyStickDisplay(false);
    }

    //ジョイスティックの表示
    private void JoyStickDisplay(bool x)
    {
        backGround.SetActive(x);
        joyStick.SetActive(x);
    }

    //ジョイスティックの動き
    public void Move(BaseEventData data)
    {
        //取得したデータからPointerEventDataを格納する
        PointerEventData pointer = data as PointerEventData;

        //ジョイスティックとタッチされた位置の差を格納
        float x = backGround.transform.position.x - pointer.position.x;
        float y = backGround.transform.position.y - pointer.position.y;

        //backGroundの中心を元に、タッチされた位置との角度を求める
        float angle = Mathf.Atan2(y, x);

        //タッチ位置の判定し、stickMovementの範囲ないに収める
        if(Vector2.Distance(backGround.transform.position, pointer.position) > stickMovement)
        {
            y = stickMovement * Mathf.Sin(angle);
            x = stickMovement * Mathf.Cos(angle);
        }

        //プレイヤーを動かす値を格納
        joyStickPosX = -x / stickMovement;
        joyStickPosY = -y / stickMovement;

        //ジョイスティックの位置
        joyStick.transform.position = new Vector2(backGround.transform.position.x - x, backGround.transform.position.y - y);

    }
    //入力中に呼ぶ関数
    public void PointerDown(BaseEventData data)
    {
        //取得したデータからPointerEventDataを格納する
        PointerEventData pointer = data as PointerEventData;

        //ジョイスティックの表示
        JoyStickDisplay(true);

        //ジョイスティックがタッチされた場所に表示される
        backGround.transform.position = pointer.position;
    }

    //指を離した瞬間に呼ぶ関数
    public void PointerUp(BaseEventData data)
    {
        //ジョイスティックのポジション初期化関数を呼ぶ
        PositionInitialization();

        //ジョイスティックの非表示
        JoyStickDisplay(false);
    }

    //ジョイスティックのポジション初期化
    public void PositionInitialization()
    {
        //x軸y軸を0にする
        joyStickRectTransform.anchoredPosition = Vector2.zero;

        //指を離したらプレイヤーが止まるように値を0にする
        joyStickPosX = 0;
        joyStickPosY = 0;
    }
}
