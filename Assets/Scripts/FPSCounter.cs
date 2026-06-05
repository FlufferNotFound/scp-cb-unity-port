using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private float fps;

    public Text FPSText;

    public float updateFrequency = 1f;

    void Awake()
    {
        InvokeRepeating(nameof(CountFPS), 0f, updateFrequency);
    }

    private void CountFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);

        if(fps >= 60) {
            FPSText.color = Color.green;
        } else if (fps >= 30) {
            FPSText.color = Color.yellow;
        } else {
            FPSText.color = Color.red;
        }

        FPSText.text = "FPS: " + fps.ToString();
    }
}
