using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Material[] sky;
    int num = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            num += 1;
        }

        if (num >= sky.Length)
        {
            num = 0;
        }

        RenderSettings.skybox = sky[num];
    }
}
