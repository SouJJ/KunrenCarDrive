using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    void Start()
    {
        // フレームレートを60に設定
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Rキーを押すと現在のシーンを再読み込み
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        // 現在のシーンのインデックスを取得し、再読み込み
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

