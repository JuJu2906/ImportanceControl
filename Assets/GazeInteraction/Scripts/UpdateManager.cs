#region Includes
using UnityEngine;
#endregion

/// <summary>
/// Manages the updates of Importance and Frequency of each window respectively.
/// </summary>
public class UpdateManager : MonoBehaviour
{
    #region Variables

    [Tooltip("The list of Game Objects that are updated")]
    public WindowGazeData[] windows;
    
    [Header("Hyperparameters")]

    [Tooltip("The decay factor for Frequencey. Lower values mean slower decay.")]
    public float forgetFactorFreq = 0.01f;

    [Tooltip("The decay factor for Importance. Lower values mean slower decay.")]
    public float forgetFactorImp = 0.05f;

    [Tooltip("The learning weight for Frequency. Higher values mean more impact of the Importance by gaze-enter.")]
    public float weightFreq = 0.2f;

    [Tooltip("The learning weight for Dwell Time. Higher values mean more impact of the Importance by gaze-stay.")]
    public float weightDwell = 0.8f;

    [Tooltip("The learning rate of the Importance update. Higher values mean faster growth and higher upper limit upon indefinite gaze-stay.")]
    public float learningRate = 0.5f;

    private float interval = 0.5f;
    private float timer = 0f;

    #endregion

    /// <summary>
    /// Gets the scale of each window respectively. The scale is used to know the 100% size of the windows.
    /// </summary>
    void Start(){
        foreach (WindowGazeData window in windows){
            window.UpdateScale();
        }
    }

    /// <summary>
    /// Updates Frequency and Importance every 0.5 second of each Game Object respectively.
    /// </summary>
    void Update(){
        timer += Time.deltaTime;

        if (timer >= interval){
            foreach (WindowGazeData window in windows){
                window.UpdateFrequency(Time.time, forgetFactorFreq);
                window.UpdateImportance(timer, forgetFactorImp, weightFreq, weightDwell, learningRate);
            }
            timer = 0f;
        }
    }
}
