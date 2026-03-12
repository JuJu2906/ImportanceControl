using System.Collections.Generic;
using AUIT.AdaptationObjectives.Definitions;
using UnityEngine;


namespace AUIT.PropertyTransitions{
	public class OpacityTransition : PropertyTransition
{
    protected override TransitionType TransitionType => TransitionType.Alpha;

	public override void Adapt(Layout layout){
		GetComponent<CanvasGroup>().alpha = layout.Alpha;
		transform.localScale = layout.Scale;
	}
}
}