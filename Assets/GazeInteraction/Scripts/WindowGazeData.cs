using UnityEngine;

public class WindowGazeData : MonoBehaviour
{
    public bool gazeEnter = false;
    public bool gazeStay = false;

    private float indicatorFreq = 0f;
    private float indicatorDwell = 0f;

    public float lastTimestamp = 0f;

    public float frequency = 0f;
    public float importance = 0f;

    public float windowScale = 1f;

    public void UpdateScale(){
        windowScale = transform.localScale.x;
    }

    public void UpdateGazeEnter(){
        gazeEnter = true;
        gazeStay = true;
        Debug.Log("Gaze Enter is set to: " + gazeEnter);
    }

    public void UpdateGazeExit(){
        gazeStay = false;
    }

    public void UpdateFrequency(float currentTime, float forgetFactorFreq){
        if (gazeEnter) indicatorFreq = 1f;
        frequency = frequency * Mathf.Exp(-1f * forgetFactorFreq * (currentTime - lastTimestamp));
        Debug.Log("Frequency Delta Time" + (currentTime - lastTimestamp));
        frequency += indicatorFreq;
        if (gazeEnter) lastTimestamp = currentTime;
        gazeEnter = false;
        indicatorFreq = 0f;
        Debug.Log("Frequency: " + frequency);
    }

    public void UpdateImportance(float deltaTime, float forgetFactorImp, float weightFreq, float weightDwell, float updateRate){
        if (gazeStay) indicatorDwell = 1f;
        importance = importance * Mathf.Exp(-forgetFactorImp * deltaTime) + updateRate * (frequency * weightFreq + indicatorDwell * weightDwell);
        Debug.Log("Importance: " + importance);

        indicatorDwell = 0f;
    }
}
