using System.Collections;
using AUIT.AdaptationObjectives.Definitions;
using UnityEngine;


namespace AUIT.PropertyTransitions{
	public class OpacityTransition : PropertyTransition
	{
		public float duration = 0.49f;
		private bool _adapting = false;
		private float _currentAlpha;
		private float _targetAlpha;
		protected override TransitionType TransitionType => TransitionType.Alpha;

		public override void Adapt(Layout layout){
			transform.localScale = layout.Scale;

			if (_adapting) return;
			_currentAlpha = GetComponent<CanvasGroup>().alpha;
			_targetAlpha = layout.Alpha;

			_adapting = true;
			StartCoroutine(AnimateAlphaTransition());
		}

		private IEnumerator AnimateAlphaTransition()
		{
			float elapsed = 0f;
			float deltaAlpha = _currentAlpha - _targetAlpha;

			while(elapsed < duration){
				float t = Time.deltaTime/duration;
				GetComponent<CanvasGroup>().alpha -= deltaAlpha * t;
				elapsed +=Time.deltaTime;
				Debug.Log("Elapsed Time: " + elapsed);
				yield return null;
			}
			GetComponent<CanvasGroup>().alpha = _targetAlpha;
			_adapting = false;
		}
	}
}