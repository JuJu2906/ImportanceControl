using UnityEngine;

public class MyGazeInteraction : MonoBehaviour
{
	//public GameObject window;

	public void OnGazeEnter(){
		Debug.Log("GazeEnter: " + gameObject.name);
		GetComponentInParent<WindowGazeData>().UpdateGazeEnter();
	}

	public void OnGazeExit(){
		Debug.Log("Gaze Exit: " + gameObject.name);
		GetComponentInParent<WindowGazeData>().UpdateGazeExit();
	}
}

