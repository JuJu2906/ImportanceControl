using System.Collections;
using AUIT.AdaptationObjectives.Definitions;
using UnityEngine;


namespace AUIT.PropertyTransitions{
	public class OpacityTransition : PropertyTransition
	{
		public float duration = 0.5f;
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

			while(elapsed < duration){
				float t = elapsed/duration;
				float deltaAlpha = _currentAlpha - _targetAlpha;
				GetComponent<CanvasGroup>().alpha += deltaAlpha * t;
				elapsed +=Time.deltaTime;
				yield return null;
			}
			_currentAlpha = _targetAlpha;
			_adapting = false;
		}
	}
}