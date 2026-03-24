using System.Collections;
using AUIT.AdaptationObjectives.Definitions;
using UnityEngine;


namespace AUIT.PropertyTransitions{
	public class ImportanceTransition : PropertyTransition
	{
		public float duration = 0.49f;
		private bool _adapting = false;
		private float _currentAlpha;
		private float _targetAlpha;
		protected override TransitionType TransitionType => TransitionType.Alpha;

		public override void Adapt(Layout layout){
			if (_adapting) return;
			_currentAlpha = GetComponent<CanvasGroup>().alpha;
			_targetAlpha = layout.Alpha;

			_adapting = true;
			StartCoroutine(AnimateAlphaTransition(layout));
		}

		private IEnumerator AnimateAlphaTransition(Layout layout)
		{
			float elapsed = 0f;
			float deltaAlpha = _targetAlpha - _currentAlpha;

			float targetScale = layout.Scale.x;
			float currentScale = transform.localScale.x;
			float deltaScale = targetScale - currentScale;

			if (deltaAlpha >= 0f)
				duration = 0.2f;

			while(elapsed < duration){
				float t = Time.deltaTime/duration;
				GetComponent<CanvasGroup>().alpha += deltaAlpha * t;

				currentScale += deltaScale * t;
				transform.localScale = new Vector3(currentScale, currentScale, 1f);

				elapsed +=Time.deltaTime;
				Debug.Log("Elapsed Time: " + elapsed);
				yield return null;
			}
			GetComponent<CanvasGroup>().alpha = _targetAlpha;
			transform.localScale = layout.Scale;
			_adapting = false;
		}
	}
}