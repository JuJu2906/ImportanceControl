using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public WindowGazeData[] windows;
    private float timer = 0f;
    private float interval = 0.5f;

    void Start(){

    }

    void Update(){
        timer += Time.deltaTime;

        if (timer >= interval){
            foreach (WindowGazeData window in windows){
                window.UpdateFrequency(Time.time);
                window.UpdateImportance(timer);
            }
            timer = 0f;
        }
    }
}
