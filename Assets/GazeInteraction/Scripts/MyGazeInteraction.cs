using UnityEngine;

public class MyGazeInteraction : MonoBehaviour
{
	public void OnGazeEnter(){
		Debug.Log("GazeEnter: " + gameObject.name);
		GetComponent<WindowGazeData>().UpdateGazeEnter();
	}

	public void OnGazeExit(){
		Debug.Log("Gaze Exit: " + gameObject.name);
		GetComponent<WindowGazeData>().UpdateGazeExit();
	}
}

