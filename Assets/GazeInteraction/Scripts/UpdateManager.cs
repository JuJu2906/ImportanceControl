using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public WindowGazeData[] windows;
    private float timer = 0f;
    private float interval = 0.5f;

    public float forgetFactorFreq = 0.01f;
    public float forgetFactorImp = 0.05f;

    public float weightFreq = 0.2f;
    public float weightDwell = 0.8f;
    public float updateRate = 0.5f;

    void Start(){
        foreach (WindowGazeData window in windows){
            window.UpdateScale();
        }
    }

    void Update(){
        timer += Time.deltaTime;

        if (timer >= interval){
            foreach (WindowGazeData window in windows){
                window.UpdateFrequency(Time.time, forgetFactorFreq);
                window.UpdateImportance(timer, forgetFactorImp, weightFreq, weightDwell, updateRate);
            }
            timer = 0f;
        }
    }
}
