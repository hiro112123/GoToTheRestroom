/*
    BGMを遅れて再生させる
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource townBgm;
    void Start()
    {
        //BGMを3秒後から流し始める
        Invoke("AudioPlay", 3f);
    }

    public void AudioPlay()
    {
        townBgm.Play();
    }
}
