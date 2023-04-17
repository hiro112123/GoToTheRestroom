/*
    画面の右側をタップやスライドしたら視点が変わる
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class JoyStick_Cam : MonoBehaviour
{
    //変数
    //カメラの感度
    public float aimSensitivity = 10;
    //スティックの動く量
    private int stickMovement = 0;
    //向くべき方向X,Y
    public float positionX, positionY;
    //角度制限
    public float viewPointValue = 45;
    //比較用に一時的に数値を格納するX,Y
    private float tempPosX = 0, tempPosY = 0;
    //Playerに向いて欲しい回転を格納X,Y
    public static float rotX = 0, rotY = 0;
    public Quaternion playerRotation1;
    public Vector3 playerRotation2;
    
    private void Start()
    {
        //Homeでの初期向き
        if(SceneManager.GetActiveScene().name =="Home")
        {
            playerRotation1 = Quaternion.Euler(270,0,0);
            playerRotation2 = playerRotation1.eulerAngles;
            rotX = playerRotation2.x;
            rotY = playerRotation2.y;
            //Debug.Log(playerRotation2);
        }
        else if(SceneManager.GetActiveScene().name == "Town")
        {
            playerRotation1 = Quaternion.Euler(180,0,0);
            playerRotation2 = playerRotation1.eulerAngles;
            rotX = playerRotation2.y;
            rotY = playerRotation2.x;
            //Debug.Log(playerRotation2);
        }
        stickMovement = 3 * (Screen.width + Screen.height) / 100;
    }

    //関数の作成
    //右画面をドラッグしているときに呼ぶ関数
    public void Move(BaseEventData data)
    {
        //取得したデータからPointerEventDataを格納する
        PointerEventData pointer = data as PointerEventData;

        //ドラッグされた数値を変数に格納
        positionX = pointer.position.x / stickMovement;
        positionY = pointer.position.y / stickMovement;

        //感度調整
        positionX *= aimSensitivity;
        positionY *= aimSensitivity;

        //回転
        Rotation();

    }
    //回転
    public void Rotation()
    {
        if(positionX != tempPosX)
        {
            if(tempPosX == 0)
            {
                tempPosX = positionX;
            }

            if(positionX == 0)
            {
                tempPosX = 0;
            }

            rotX -= (tempPosX - positionX);

            if(rotX > 360)
            {
                rotX -= 360;
            }

            if(rotX < -360)
            {
                rotX += 360;
            }

            tempPosX = positionX;
        }

        if(positionY != tempPosY)
        {
            if(tempPosY == 0)
            {
                tempPosY = positionY;
            }

            if(positionY == 0)
            {
                tempPosY = 0;
            }

            rotY += (tempPosY - positionY);

            if(rotY > viewPointValue)
            {
                rotY = viewPointValue;
            }

            if(rotY < -viewPointValue)
            {
                rotY = -viewPointValue;
            }

            tempPosY = positionY;
        }
    }

    //右画面から指を離したときに呼ぶ関数
    public void PointerUp(BaseEventData data)
    {
        //ポジション初期化関数
        PositionInitialization();
        //回転
        Rotation();
    }

    //ポジション初期化関数
    public void PositionInitialization()
    {
        positionX = 0;
        positionY = 0;
    }
}
