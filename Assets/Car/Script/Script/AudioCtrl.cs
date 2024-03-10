using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip engineSE;
    private CarController carController; // CarControllerスクリプトへの参照

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        carController = GetComponent<CarController>(); // CarControllerスクリプトへの参照を取得
    }

    // Update is called once per frame
    void Update()
    {
        // CarControllerから速度を取得
        float speed = carController.velocity;

        if (speed >= 10f)
        {
            if (!aud.isPlaying) // 音が再生されていない場合
            {
                aud.clip = engineSE; // エンジン音のクリップをセット
                aud.Play(); // 音を再生する
            }
        }
        else
        {
            if (aud.isPlaying) // 音が再生されている場合
            {
                
            }
        }
    }
}
