using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCheck : MonoBehaviour
{
    
    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    void Start()
    {
        StartCoroutine(FPS());
    }

    private void Update()
    {
        _text.text = fps;
    }


    private IEnumerator FPS() {
        for(;;){
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;
           
            // Display it
 
            fps = string.Format("FPS: {0}" , Mathf.RoundToInt(frameCount / timeSpan));
            
        }
    }
}
