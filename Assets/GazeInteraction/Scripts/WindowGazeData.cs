#region Includes
using UnityEngine;
#endregion

/// <summary>
/// Stores gaze-related interaction data for a Game Object and computes
/// Frequency and Importance over time.
/// </summary>
public class WindowGazeData : MonoBehaviour
{
    #region Variables

    [Tooltip("Enables debug logging for frequency and importance updates.")]
    public bool debug = false;

    [Header("Window attributes")]

    [Tooltip("Stores the current scale of the window. Assumes x-scale and y-scale are equal")]
    public float windowScale = 1f;

    [Header("Gaze Data")]

    [Tooltip("Represents how often gaze entered including exponential decay over time.")]
    public float frequency = 0f;

    [Tooltip("Represents the importance of the object. It is influenced by frequency and dwell time")]
    public float importance = 0f;

    [Tooltip("True while the user is looking at the game object.")]
    public bool gazeStay = false;

    private bool gazeEnter = false;
    private float indicatorFreq = 0f;
    private float indicatorDwell = 0f;
    private float lastTimestamp = 0f;

    #endregion

    public void UpdateScale(){
        windowScale = transform.localScale.x;
    }

    public void UpdateGazeEnter(){
        gazeEnter = true;
        gazeStay = true;
    }

    public void UpdateGazeExit(){
        gazeStay = false;
    }

    /// <summary>
    /// Updates the gaze frequency value using exponential decay and gaze-enter events.
    /// </summary>
    /// <param name="currentTime"></param>
    /// <param name="forgetFactorFreq"></param>
    public void UpdateFrequency(float currentTime, float forgetFactorFreq){
        if (gazeEnter) indicatorFreq = 1f;
        frequency = frequency * Mathf.Exp(-1f * forgetFactorFreq * (currentTime - lastTimestamp));
        frequency += indicatorFreq;
        if (gazeEnter) lastTimestamp = currentTime;
        gazeEnter = false;
        indicatorFreq = 0f;
        if (debug) Debug.Log("Frequency: " + frequency);
    }

    /// <summary>
    /// Updates the Importance value using exponential decay and weighted gaze signals(Frequency and Dwell Time).
    /// </summary>
    /// <param name="deltaTime">
    /// <paramref name="forgetFactorImp"/>
    /// <paramref name="weightFreq"/>
    /// <paramref name="weightDwell"/>
    /// <paramref name="learningRate"/>
    public void UpdateImportance(float deltaTime, float forgetFactorImp, float weightFreq, float weightDwell, float learningRate){
        if (gazeStay) indicatorDwell = 1f;
        importance = importance * Mathf.Exp(-forgetFactorImp * deltaTime) + learningRate * (frequency * weightFreq + indicatorDwell * weightDwell);
        indicatorDwell = 0f;

        if (debug) Debug.Log("Importance: " + importance);
    }
}
