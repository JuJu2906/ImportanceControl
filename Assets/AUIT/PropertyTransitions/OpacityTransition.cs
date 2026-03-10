using System.Collections.Generic;
using AUIT.AdaptationObjectives.Definitions;
using UnityEngine;


namespace AUIT.PropertyTransitions{
	public class OpacityTransition : PropertyTransition
{
    protected override TransitionType TransitionType => TransitionType.Alpha;

	public override void Adapt(Layout layout){
		Debug.Log("Layout Alpha: " + layout.Alpha);
		bool focused = GetComponent<WindowGazeData>().gazeStay;
		GetComponent<CanvasGroup>().alpha = focused ? 1f : layout.Alpha;
	}
}
}