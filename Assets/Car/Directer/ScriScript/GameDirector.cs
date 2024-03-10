using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    void Start()
    {
        // �t���[�����[�g��60�ɐݒ�
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // R�L�[�������ƌ��݂̃V�[�����ēǂݍ���
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        // ���݂̃V�[���̃C���f�b�N�X���擾���A�ēǂݍ���
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

