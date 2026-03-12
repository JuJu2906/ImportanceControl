using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public WindowGazeData[] windows;
    private float timer = 0f;
    private float interval = 0.5f;

    public float forgetFactorFreq = 0.5f;
    public float forgetFactorImp = 0.1f;

    public float weightFreq = 1f;
    public float weightDwell = 0.5f;
    public float updateRate = 0.5f;

    void Start(){

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
