/*
    走る、しゃがむの設定
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAndSquat : MonoBehaviour
{
    //移動スピード
    public static float speed = 5f;
    //playerの背の高さ
    public static float playerHeight = 1.5f;
    //立っているかのフラグ
    public static bool standing = true;
    //走っているかのフラグ
    public static bool running = false;


    //走る
    public void Run()
    {
        speed = 8f;
        running = true;
    }

    //歩く
    public void Walk()
    {
        speed = 5f;
        running =false;
    }

    //しゃがむ
    public void Squat()
    {
        playerHeight = 1f;
        standing = false;
    }

    //立つ
    public void Stand()
    {
        playerHeight = 1.5f;
        standing = true;
    }
}
