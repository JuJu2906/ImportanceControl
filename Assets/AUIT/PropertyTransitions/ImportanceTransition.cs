#region Includes
using System.Collections;
using AUIT.AdaptationObjectives.Definitions;
using UnityEngine;
#endregion

namespace AUIT.PropertyTransitions{
	/// <summary>
	/// Handles the visual transition for a UI element based on Importance.
	/// </summary>
	public class ImportanceTransition : PropertyTransition
	{
		#region Variables
		
		private float duration = 0.5f;


		private bool _adapting = false;
		private float _currentAlpha;
		private float _targetAlpha;
		#endregion 
		protected override TransitionType TransitionType => TransitionType.Visiblity;

		/// <summary>
		/// Starts adapting the UI element toward the values defined in the given layout.
		/// If a previous transition is still in progress, it will ignore the new layout.
		/// </summary>
		/// <param name="layout"></param>
		public override void Adapt(Layout layout){
			if (_adapting) return;
			_currentAlpha = GetComponent<CanvasGroup>().alpha;
			_targetAlpha = layout.Alpha;

			_adapting = true;
			StartCoroutine(AnimateAlphaTransition(layout));
		}

		/// <summary>
		/// Animates alpha (opacity value) and scale from their current values to the target layout values over multiple frames.
		/// </summary>
		/// <param name="layout"></param>
		/// <returns></returns>
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
				yield return null;
			}
			GetComponent<CanvasGroup>().alpha = _targetAlpha;
			transform.localScale = layout.Scale;
			_adapting = false;
		}
	}
}