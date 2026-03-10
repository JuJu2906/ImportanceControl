using UnityEngine;

public class WindowGazeData : MonoBehaviour
{
    public bool gazeEnter = false;
    public bool gazeStay = false;
    public float forgetFactorFreq = 0.5f;
    public float forgetFactorImp = 0.1f;
    public float indicatorFreq = 0f;
    public float indicatorDwell = 0f;

    public float weightFreq = 1f;
    public float weightDwell = 0.5f;
    public float updateRate = 0.5f;
    public float lastTimestamp = 0f;

    public float frequency = 0f;
    public float importance = 0f;

    public void UpdateGazeEnter(){
        gazeEnter = true;
        gazeStay = true;
        Debug.Log("Gaze Enter is set to: " + gazeEnter);
    }

    public void UpdateGazeExit(){
        gazeStay = false;
    }

    public void UpdateFrequency(float currentTime){
        if (gazeEnter) indicatorFreq = 1f;
        frequency = frequency * Mathf.Exp(-1f * forgetFactorFreq * (currentTime - lastTimestamp)/(currentTime - lastTimestamp + 10));
        frequency += indicatorFreq;
        gazeEnter = false;
        if (gazeEnter) lastTimestamp = currentTime;
        indicatorFreq = 0f;
        Debug.Log("Frequency: " + frequency);
    }

    public void UpdateImportance(float deltaTime){
        if (gazeStay) indicatorDwell = 1f;
        importance = importance * Mathf.Exp(-forgetFactorImp * deltaTime) + updateRate * (frequency * weightFreq + indicatorDwell * weightDwell);
        Debug.Log("Importance: " + importance);

        indicatorDwell = 0f;
    }
}
