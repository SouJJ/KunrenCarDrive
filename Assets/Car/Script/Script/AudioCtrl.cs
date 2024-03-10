using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip engineSE;
    private CarController carController; // CarController�X�N���v�g�ւ̎Q��

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        carController = GetComponent<CarController>(); // CarController�X�N���v�g�ւ̎Q�Ƃ��擾
    }

    // Update is called once per frame
    void Update()
    {
        // CarController���瑬�x���擾
        float speed = carController.velocity;

        if (speed >= 10f)
        {
            if (!aud.isPlaying) // �����Đ�����Ă��Ȃ��ꍇ
            {
                aud.clip = engineSE; // �G���W�����̃N���b�v���Z�b�g
                aud.Play(); // �����Đ�����
            }
        }
        else
        {
            if (aud.isPlaying) // �����Đ�����Ă���ꍇ
            {
                
            }
        }
    }
}
